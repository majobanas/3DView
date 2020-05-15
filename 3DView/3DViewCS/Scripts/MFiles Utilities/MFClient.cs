// M-Files Web Service client.
//
// This sample code defines a simple M-Files Web Service client which can
// be used to perform requests against an M-Files Web Service.
//
// The file also contains MFiles.Mfws.Structs namespace which defines the
// types used with the web service.


// Copyright 2012 M-Files Corporation
// http://www.m-files.com/
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Globalization;
using System.IO;
using ThreeDViewCS;

// Requires System.Web;
using System.Net;

// Requires System.Runtime.Serialization reference.
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace MFiles.MFWS {



    public class MFClient {
        public string EndPoint {
            get; private set;
        }

        public string Authentication {
            get; set;
        }

        public MFClient(string pEndPoint) {
            if (!pEndPoint.EndsWith("/"))
                pEndPoint += "/";

            this.EndPoint = pEndPoint;
            this.Authentication = null;
        }

        public T Get<T>(string pPath) {
            return (T)PerformRequest("GET", pPath, null, typeof(T));
        }

        public T Post<T>(string pPath, object pContent) {
            return (T)PerformRequest("POST", pPath, pContent, typeof(T));
        }

        public T Put<T>(string pPath, object pContent) {
            return (T)PerformRequest("PUT", pPath, pContent, typeof(T));
        }

        public void Delete(string pPath) {
            PerformRequest("DELETE", pPath, null, null);
        }

        public Resource Resource(string pPattern, params object[] pArguments) {
            return new Resource(this, string.Format(pPattern, pArguments));
        }

        private object PerformRequest(string pMethod, string pPath, object pContent, Type pReturnType) {
            Debug.Log("C# MFClient.PerformRequest<" + pMethod + ">: " + pPath);
            if (pPath[0] == '/')
                pPath = pPath.Substring(1);
            string uri = EndPoint + pPath;
            
            if (pMethod == "PUT" || pMethod == "DELETE") {
                if (uri.Contains("?"))
                    uri += "&";
                else
                    uri += "?";
                uri += "_method=" + pMethod;
            }

            WebRequest request = WebRequest.Create(uri);
            
            request.Method = (pMethod.ToUpper() == "GET") ? "GET" : "POST";

            if (!string.IsNullOrEmpty(Authentication))
                request.Headers["X-Authentication"] = Authentication;

            if (pContent != null) {
                request.ContentType = "text/plain";
                //request.ContentType = "application/octet-stream";
                if (pContent is Stream) {
                    Stream requestStream = request.GetRequestStream();
                    Stream sourceStream = (Stream)pContent;
                    sourceStream.CopyTo(requestStream);
                    requestStream.Flush();
                    requestStream.Close();
                }
                else {
                    Stream requestStream = request.GetRequestStream();
                    DataContractJsonSerializer requestSerializer = new DataContractJsonSerializer(pContent.GetType());
                    requestSerializer.WriteObject(requestStream, pContent);
                    requestStream.Flush();
                    requestStream.Close();
                }
                //Console.WriteLine("UPLOAD SUCCESSFUL");
            }
            WebResponse response;
            try {
                response = request.GetResponse();

            } catch (WebException pException) {
                HandleError(pException);
                throw;
            }
            //Console.WriteLine("RESPONSE SUCCESSFUL");

            if (pReturnType == null)
                return null;

            if (pReturnType == typeof(string)) {
                return new StreamReader(response.GetResponseStream()).ReadToEnd();
            }

            if (pReturnType == typeof(Stream))
                return response.GetResponseStream();

            Stream responseStream = response.GetResponseStream();
            if (responseStream == null)
                return null;

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(pReturnType);
            return serializer.ReadObject(responseStream);
        }

        public void HandleError(WebException pWebException) {
            Stream stream = pWebException.Response.GetResponseStream();
            if (stream == null)
                return;

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Structs.WebServiceError));
            Structs.WebServiceError error = (Structs.WebServiceError)serializer.ReadObject(stream);

            throw new MfwsException(error.Exception);
        }
    }

    public class Resource {
        private MFClient client;

        public string Path {
            get; protected set;
        }

        public Resource(MFClient pClient, string pPath) {
            this.client = pClient;
            this.Path = pPath;
        }

        public T Get<T>() {
            return client.Get<T>(this.Path);
        }

        public T Post<T>(object data) {
            return client.Post<T>(this.Path, data);
        }

        public T Put<T>(object data) {
            return client.Put<T>(this.Path, data);
        }

        public void Delete() {
            client.Delete(this.Path);
        }
    }

    public class MfwsException : Exception {
        public MfwsException(Structs.ExceptionInfo pError)
            : base(pError.Message,
                  pError.InnerException != null
                      ? new MfwsException(pError.InnerException)
                      : null) {
        }
    }
}

namespace MFiles.MFWS.Structs {
    /// <summary>
    /// Specifies the information required when creating a new object.
    /// </summary>
    [DataContract]
    public class ObjectCreationInfo {

        /// <summary>
        /// Properties for the new object.
        /// </summary>
        [DataMember]
        public PropertyValue[] PropertyValues {
            get; set;
        }

        /// <summary>
        /// References previously uploaded files.
        /// </summary>
        [DataMember]
        public UploadInfo[] Files {
            get; set;
        }

    }



    /// <summary>
    /// Contains the information on a temporary upload.
    /// </summary>
    [DataContract]
    public class UploadInfo {

        /// <summary>
        /// Temporary upload ID given by the server.
        /// </summary>
        [DataMember]
        public int UploadID {
            get; set;
        }

        /// <summary>
        /// File name without extension.
        /// </summary>
        [DataMember]
        public string Title {
            get; set;
        }

        /// <summary>
        /// File extension.
        /// </summary>
        [DataMember]
        public string Extension {
            get; set;
        }

        /// <summary>
        /// File size.
        /// </summary>
        [DataMember]
        public long Size {
            get; set;
        }

    }



    /// <summary>
    /// A &#39;typed value&#39; represents a value, such as text, number, date or lookup item.
    /// </summary>
    [DataContract]
    public class TypedValue {

        /// <summary>
        /// Specifies the type of the value.
        /// </summary>
        [DataMember]
        public MFDataType DataType {
            get; set;
        }

        /// <summary>
        /// Specifies whether the typed value contains a real value.
        /// </summary>
        [DataMember]
        public bool HasValue {
            get; set;
        }

        /// <summary>
        /// Specifies the string, number or boolean value when the DataType is not a lookup type.
        /// </summary>
        [DataMember]
        public object Value {
            get; set;
        }

        /// <summary>
        /// Specifies the lookup value when the DataType is Lookup.
        /// </summary>
        [DataMember]
        public Lookup Lookup {
            get; set;
        }

        /// <summary>
        /// Specifies the collection of \type{Lookup}s when the DataType is MultiSelectLookup.
        /// </summary>
        [DataMember]
        public Lookup[] Lookups {
            get; set;
        }

        /// <summary>
        /// Provides the value formatted for display.
        /// </summary>
        [DataMember]
        public string DisplayValue {
            get; set;
        }

        /// <summary>
        /// Provides a key that can be used to sort \type{TypedValue}s
        /// </summary>
        [DataMember]
        public string SortingKey {
            get; set;
        }

        /// <summary>
        /// Provides the typed value in a serialized format suitable to be used in URIs.
        /// </summary>
        [DataMember]
        public string SerializedValue {
            get; set;
        }

    }



    /// <summary>
    /// An object version with extended properties. Inherits from ObjectVersion.
    /// </summary>
    [DataContract]
    public class ExtendedObjectVersion {

        /// <summary>
        /// Object properties
        /// </summary>
        [DataMember]
        public PropertyValue[] Properties {
            get; set;
        }

    }



    /// <summary>
    /// Authentication details.
    /// </summary>
    [DataContract]
    public class Authentication {

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Username {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Password {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Domain {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool WindowsUser {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string ComputerName {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string VaultGuid {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Expiration {
            get; set;
        }

        public DateTime? ExpirationDate {
            get {
                if (string.IsNullOrEmpty(Expiration))
                    return null;
                return DateTime.ParseExact(Expiration, "o", CultureInfo.InvariantCulture);
            }
            set {
                Expiration = (value == null) ? null : value.Value.ToString("o");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool ReadOnly {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string URL {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Method {
            get; set;
        }

    }



    /// <summary>
    /// Vault information.
    /// </summary>
    [DataContract]
    public class Vault {

        /// <summary>
        /// Vault name.
        /// </summary>
        [DataMember]
        public string Name {
            get; set;
        }

        /// <summary>
        /// Vault GUID.
        /// </summary>
        [DataMember]
        public string GUID {
            get; set;
        }

        /// <summary>
        /// Vault-specific authentication token.
        /// </summary>
        [DataMember]
        public string Authentication {
            get; set;
        }

    }



    /// <summary>
    /// Information required for changing password.
    /// </summary>
    [DataContract]
    public class PasswordChange {

        /// <summary>
        /// The current password.
        /// </summary>
        [DataMember]
        public string OldPassword {
            get; set;
        }

        /// <summary>
        /// The new password.
        /// </summary>
        [DataMember]
        public string NewPassword {
            get; set;
        }

    }



    /// <summary>
    /// Server public key information.
    /// </summary>
    [DataContract]
    public class PublicKey {

        /// <summary>
        /// Base64URL encoded exponent.
        /// </summary>
        [DataMember]
        public string Exponent {
            get; set;
        }

        /// <summary>
        /// Base64URL encoded modulus.
        /// </summary>
        [DataMember]
        public string Modulus {
            get; set;
        }

    }



    /// <summary>
    /// Response for status requests.
    /// </summary>
    [DataContract]
    public class StatusResponse {

        /// <summary>
        /// The result of the status request.
        /// </summary>
        [DataMember]
        public bool Successful {
            get; set;
        }

        /// <summary>
        /// Display message for the status.
        /// </summary>
        [DataMember]
        public string Message {
            get; set;
        }

    }



    /// <summary>
    /// An object class with extended properties. Inherits from ObjectClass.
    /// </summary>
    [DataContract]
    public class ExtendedObjectClass {

        /// <summary>
        /// Property definitions associated with this class.
        /// </summary>
        [DataMember]
        public AssociatedPropertyDef[] AssociatedPropertyDefs {
            get; set;
        }

        /// <summary>
        /// Templates available for use with this class.
        /// </summary>
        [DataMember]
        public ObjectVersion[] Templates {
            get; set;
        }

    }



    /// <summary>
    /// Workflow state information.
    /// </summary>
    [DataContract]
    public class WorkflowState {

        /// <summary>
        /// Workflow state name.
        /// </summary>
        [DataMember]
        public string Name {
            get; set;
        }

        /// <summary>
        /// Workflow state ID.
        /// </summary>
        [DataMember]
        public int ID {
            get; set;
        }

        /// <summary>
        /// Defines whether this state is selectable for the current object.
        /// </summary>
        [DataMember]
        public bool Selectable {
            get; set;
        }

    }



    /// <summary>
    /// An object version with extended properties. Inherits from ObjectVersion.
    /// </summary>
    [DataContract]
    public class FolderContentItems {

        /// <summary>
        /// The path to the current folder.
        /// </summary>
        [DataMember]
        public string Path {
            get; set;
        }

        /// <summary>
        /// Specifies whether there are more results in the folder.
        /// </summary>
        [DataMember]
        public bool MoreResults {
            get; set;
        }

        /// <summary>
        /// The actual folder contents.
        /// </summary>
        [DataMember]
        public FolderContentItem[] Items {
            get; set;
        }

    }



    /// <summary>
    /// A workflow state on an object.
    /// </summary>
    [DataContract]
    public class ObjectWorkflowState {

        /// <summary>
        /// The workflow state defined as a property value.
        /// </summary>
        [DataMember]
        public PropertyValue State {
            get; set;
        }

        /// <summary>
        /// The workflow state ID.
        /// </summary>
        [DataMember]
        public int StateID {
            get; set;
        }

        /// <summary>
        /// The workflow state name.
        /// </summary>
        [DataMember]
        public string StateName {
            get; set;
        }

        /// <summary>
        /// The workflow defined as a property value.
        /// </summary>
        [DataMember]
        public PropertyValue Workflow {
            get; set;
        }

        /// <summary>
        /// The workflow ID.
        /// </summary>
        [DataMember]
        public int WorkflowID {
            get; set;
        }

        /// <summary>
        /// The workflow name.
        /// </summary>
        [DataMember]
        public string WorkflowName {
            get; set;
        }

        /// <summary>
        /// Version comment defined on the workflow transition.
        /// </summary>
        [DataMember]
        public string VersionComment {
            get; set;
        }

    }



    /// <summary>
    /// M-Files Web Service error object.
    /// </summary>
    [DataContract]
    public class WebServiceError {

        /// <summary>
        /// HTTP Status code
        /// </summary>
        [DataMember]
        public int Status {
            get; set;
        }

        /// <summary>
        /// The request URL which caused this error.
        /// </summary>
        [DataMember]
        public string URL {
            get; set;
        }

        /// <summary>
        /// The request method.
        /// </summary>
        [DataMember]
        public string Method {
            get; set;
        }

        /// <summary>
        /// Detailed information on the exception.
        /// </summary>
        [DataMember]
        public ExceptionInfo Exception {
            get; set;
        }

    }



    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class ExceptionInfo {

        /// <summary>
        /// Error message.
        /// </summary>
        [DataMember]
        public string Message {
            get; set;
        }

        /// <summary>
        /// Underlying error that caused this one.
        /// </summary>
        [DataMember]
        public ExceptionInfo InnerException {
            get; set;
        }

        /// <summary>
        /// M-Files Web Service server-side stack trace.
        /// </summary>
        [DataMember]
        public StackTraceElement[] Stack {
            get; set;
        }

    }



    /// <summary>
    /// M-Files Web Service error stack trace element.
    /// </summary>
    [DataContract]
    public class StackTraceElement {

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string FileName {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int LineNumber {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string ClassName {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string MethodName {
            get; set;
        }

    }



    /// <summary>
    /// Results of a query which might leave only a partial set of items.
    /// </summary>
    [DataContract]
    public class Results<T> {

        /// <summary>
        /// Contains results of a query
        /// </summary>
        [DataMember]
        public T[] Items {
            get; set;
        }

        /// <summary>
        /// True if there were more results which were left out.
        /// </summary>
        [DataMember]
        public bool MoreResults {
            get; set;
        }

    }



    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class PrimitiveType<T> {

        /// <summary>
        /// Primitive value.
        /// </summary>
        [DataMember]
        public T Value {
            get; set;
        }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    [DataContract]
    public class ObjectVersion {

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public string AccessedByMeUtc {
            get; set;
        }

        public DateTime? AccessedByMeUtcDate {
            get {
                if (string.IsNullOrEmpty(AccessedByMeUtc))
                    return null;
                return DateTime.ParseExact(AccessedByMeUtc, "o", CultureInfo.InvariantCulture);
            }
            set {
                AccessedByMeUtc = (value == null) ? null : value.Value.ToString("o");
            }
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public string CheckedOutAtUtc {
            get; set;
        }

        public DateTime? CheckedOutAtUtcDate {
            get {
                if (string.IsNullOrEmpty(CheckedOutAtUtc))
                    return null;
                return DateTime.ParseExact(CheckedOutAtUtc, "o", CultureInfo.InvariantCulture);
            }
            set {
                CheckedOutAtUtc = (value == null) ? null : value.Value.ToString("o");
            }
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int CheckedOutTo {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public string CheckedOutToUserName {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int Class {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public string CreatedUtc {
            get; set;
        }

        public DateTime? CreatedUtcDate {
            get {
                if (string.IsNullOrEmpty(CreatedUtc))
                    return null;
                return DateTime.ParseExact(CreatedUtc, "o", CultureInfo.InvariantCulture);
            }
            set {
                CreatedUtc = (value == null) ? null : value.Value.ToString("o");
            }
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool Deleted {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public string DisplayID {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public ObjectFile[] Files {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool HasAssignments {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool HasRelationshipsFromThis {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool HasRelationshipsToThis {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool IsStub {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public string LastModifiedUtc {
            get; set;
        }

        public DateTime? LastModifiedUtcDate {
            get {
                if (string.IsNullOrEmpty(LastModifiedUtc))
                    return null;
                return DateTime.ParseExact(LastModifiedUtc, "o", CultureInfo.InvariantCulture);
            }
            set {
                LastModifiedUtc = (value == null) ? null : value.Value.ToString("o");
            }
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool ObjectCheckedOut {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool ObjectCheckedOutToThisUser {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public MFObjectVersionFlag ObjectVersionFlags {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public ObjVer ObjVer {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool SingleFile {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool ThisVersionLatestToThisUser {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public string Title {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool VisibleAfterOperation {
            get; set;
        }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    [DataContract]
    public class ObjectFile {

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public string ChangeTimeUtc {
            get; set;
        }

        public DateTime? ChangeTimeUtcDate {
            get {
                if (string.IsNullOrEmpty(ChangeTimeUtc))
                    return null;
                return DateTime.ParseExact(ChangeTimeUtc, "o", CultureInfo.InvariantCulture);
            }
            set {
                ChangeTimeUtc = (value == null) ? null : value.Value.ToString("o");
            }
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public string Extension {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int ID {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public string Name {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int Version {
            get; set;
        }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    [DataContract]
    public class ObjVer {

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int ID {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int Type {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int Version {
            get; set;
        }

        public override string ToString() {
            return "" + Type + Parser.itemSplitter + ID + Parser.itemSplitter + Version;
        }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    [DataContract]
    public class PropertyValue {

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int PropertyDef {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public TypedValue TypedValue {
            get; set;
        }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    [DataContract]
    public class SessionInfo {

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public string AccountName {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public MFACLMode ACLMode {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public MFAuthType AuthenticationType {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool CanForceUndoCheckout {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool CanManageCommonUISettings {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool CanManageCommonViews {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool CanManageTraditionalFolders {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool CanMaterializeViews {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool CanSeeAllObjects {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool CanSeeDeletedObjects {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool InternalUser {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool LicenseAllowsModifications {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int UserID {
            get; set;
        }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    [DataContract]
    public class ObjType {

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool AllowAdding {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool CanHaveFiles {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int DefaultPropertyDef {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool External {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int ID {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public string NamePlural {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public string Name {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int OwnerPropertyDef {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int[] ReadOnlyPropertiesDuringInsert {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int[] ReadOnlyPropertiesDuringUpdate {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool RealObjectType {
            get; set;
        }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    [DataContract]
    public class PropertyDef {

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool AllObjectTypes {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public string AutomaticValue {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public MFAutomaticValueType AutomaticValueType {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool BasedOnValueList {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public MFDataType DataType {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int ID {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public string Name {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int ObjectType {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int ValueList {
            get; set;
        }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    [DataContract]
    public class Workflow {

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int ID {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public string Name {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int ObjectClass {
            get; set;
        }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    [DataContract]
    public class ValueListItem {

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public string DisplayID {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool HasOwner {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool HasParent {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int ID {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public string Name {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int OwnerID {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int ParentID {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int ValueListID {
            get; set;
        }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    [DataContract]
    public class ObjID {

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int ID {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int Type {
            get; set;
        }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    [DataContract]
    public class Lookup {

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool Deleted {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public string DisplayValue {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool Hidden {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int Item {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int Version {
            get; set;
        }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    [DataContract]
    public class VersionComment {

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public PropertyValue LastModifiedBy {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public ObjVer ObjVer {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public PropertyValue StatusChanged {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public PropertyValue Comment {
            get; set;
        }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    [DataContract]
    public class AssociatedPropertyDef {

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int PropertyDef {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool Required {
            get; set;
        }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    [DataContract]
    public class FolderContentItem {

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public MFFolderContentItemType FolderContentItemType {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public ObjectVersion ObjectVersion {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public TypedValue PropertyFolder {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public Lookup TraditionalFolder {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public View View {
            get; set;
        }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    [DataContract]
    public class View {

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool Common {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int ID {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public string Name {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int Parent {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public ViewLocation ViewLocation {
            get; set;
        }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    [DataContract]
    public class ViewLocation {

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public TypedValue OverlappedFolder {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public bool Overlapping {
            get; set;
        }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    [DataContract]
    public class ObjectClass {

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int ID {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public string Name {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int NamePropertyDef {
            get; set;
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public int Workflow {
            get; set;
        }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    [DataContract]
    public class ClassGroup {

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        [DataMember]
        public string Name {
            get; set;
        }

    }




    /// <summary>
    /// 
    /// </summary>
    public enum MFCheckOutStatus {


        /// <summary>
        /// Object is checked in.
        /// </summary>
        CheckedIn = 0,


        /// <summary>
        /// Object is checked out to someone else.
        /// </summary>
        CheckedOut = 1,


        /// <summary>
        /// Object is checked out to the current user.
        /// </summary>
        CheckedOutToMe = 2,

    }



    /// <summary>
    /// 
    /// </summary>
    public enum MFRefreshStatus {


        /// <summary>
        /// 
        /// </summary>
        None = 0,


        /// <summary>
        /// 
        /// </summary>
        Quick = 1,


        /// <summary>
        /// 
        /// </summary>
        Full = 2,

    }



    /// <summary>
    /// 
    /// </summary>
    public enum MFObjectVersionFlag {


        /// <summary>
        /// 
        /// </summary>
        None = 0,


        /// <summary>
        /// 
        /// </summary>
        Completed = 1,


        /// <summary>
        /// 
        /// </summary>
        HasRelatedObjects = 2,

    }



    /// <summary>
    /// 
    /// </summary>
    public enum MFAuthType {


        /// <summary>
        /// 
        /// </summary>
        Unknown = 0,


        /// <summary>
        /// 
        /// </summary>
        LoggedOnWindowsUser = 1,


        /// <summary>
        /// 
        /// </summary>
        SpecificWindowsUser = 2,


        /// <summary>
        /// 
        /// </summary>
        SpecificMFilesUser = 3,

    }



    /// <summary>
    /// 
    /// </summary>
    public enum MFACLMode {


        /// <summary>
        /// 
        /// </summary>
        Simple = 0,


        /// <summary>
        /// 
        /// </summary>
        AutomaticPermissionsWithComponents = 1,

    }



    /// <summary>
    /// 
    /// </summary>
    public enum MFDataType {


        /// <summary>
        /// 
        /// </summary>
        Uninitialized = 0,


        /// <summary>
        /// 
        /// </summary>
        Text = 1,


        /// <summary>
        /// 
        /// </summary>
        Integer = 2,


        /// <summary>
        /// 
        /// </summary>
        Floating = 3,


        /// <summary>
        /// 
        /// </summary>
        Date = 5,


        /// <summary>
        /// 
        /// </summary>
        Time = 6,


        /// <summary>
        /// 
        /// </summary>
        Timestamp = 7,


        /// <summary>
        /// 
        /// </summary>
        Boolean = 8,


        /// <summary>
        /// 
        /// </summary>
        Lookup = 9,


        /// <summary>
        /// 
        /// </summary>
        MultiSelectLookup = 10,


        /// <summary>
        /// 
        /// </summary>
        Integer64 = 11,


        /// <summary>
        /// 
        /// </summary>
        FILETIME = 12,


        /// <summary>
        /// 
        /// </summary>
        MultiLineText = 13,


        /// <summary>
        /// 
        /// </summary>
        ACL = 14,

    }



    /// <summary>
    /// 
    /// </summary>
    public enum MFAutomaticValueType {


        /// <summary>
        /// 
        /// </summary>
        None = 0,


        /// <summary>
        /// 
        /// </summary>
        CalculatedWithPlaceholders = 1,


        /// <summary>
        /// 
        /// </summary>
        CalculatedWithVBScript = 2,


        /// <summary>
        /// 
        /// </summary>
        AutoNumberSimple = 3,


        /// <summary>
        /// 
        /// </summary>
        WithVBScript = 4,

    }



    /// <summary>
    /// 
    /// </summary>
    public enum MFFolderContentItemType {


        /// <summary>
        /// 
        /// </summary>
        Unknown = 0,


        /// <summary>
        /// 
        /// </summary>
        ViewFolder = 1,


        /// <summary>
        /// 
        /// </summary>
        PropertyFolder = 2,


        /// <summary>
        /// 
        /// </summary>
        TraditionalFolder = 3,


        /// <summary>
        /// 
        /// </summary>
        ObjectVersion = 4,

    }


}
