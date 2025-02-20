﻿using System;
using System.Collections.Generic;
using MFiles.MFWS;
using MFiles.MFWS.Structs;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace ThreeDViewCS {
    public static class MFiles {

        public static Dictionary<int, string> Types = new Dictionary<int, string>();
        public static Dictionary<int, string> Properties = new Dictionary<int, string>();

        public static bool LoggedIn { get; private set; }
        public static string VaultName { get; private set; }
        public static string ClientName { get; private set; }

        private static MFClient client = null;

        public static void Login(string pURL, string pUsername, string pPassword, string pVaultGUID) {

            client = new MFClient(pURL);
            client.Authentication =
                     client.Post<PrimitiveType<string>>(
                         MFRequest.AuthenticationTokens(),
                         new Authentication { Username = pUsername, Password = pPassword, VaultGuid = pVaultGUID }
                     ).Value;



            Vault vault = client.Get<Vault>(MFRequest.Vault());
            VaultName = vault.Name;
            client.Authentication = vault.Authentication;

            SessionInfo sessionInfo = client.Get<SessionInfo>(MFRequest.Session());
            ClientName = sessionInfo.AccountName;

            if (client.Authentication != "") {
                /*SessionInfo sessionInfo = client.Get<SessionInfo>(MFRequest.Session());
                Console.WriteLine("Account name: " + sessionInfo.AccountName);
                Console.WriteLine("ACLMode: " + sessionInfo.ACLMode);
                Console.WriteLine("Authentication type: " + sessionInfo.AuthenticationType);
                Console.WriteLine("Can force undo-checkout: " + sessionInfo.CanForceUndoCheckout);
                Console.WriteLine("Can manage common UI settings: " + sessionInfo.CanManageCommonUISettings);
                Console.WriteLine("Can manage common views: " + sessionInfo.CanManageCommonViews);
                Console.WriteLine("Can manage traditional folders: " + sessionInfo.CanManageTraditionalFolders);
                Console.WriteLine("Can materialize views: " + sessionInfo.CanMaterializeViews);
                Console.WriteLine("Can see all objects: " + sessionInfo.CanSeeAllObjects);
                Console.WriteLine("Can see deleted objects: " + sessionInfo.CanSeeDeletedObjects);
                Console.WriteLine("Internal user: " + sessionInfo.InternalUser);
                Console.WriteLine("License allows modifications: " + sessionInfo.LicenseAllowsModifications);
                Console.WriteLine("User ID: " + sessionInfo.UserID);*/


                LoggedIn = true;
            }
        }

        public static void ReadProperties() {
            PropertyDef[] properties = client.Get<PropertyDef[]>(MFRequest.PropertyDefinitions());
            foreach (PropertyDef property in properties) {
                Properties.Add(property.ID, property.Name);
                //Console.WriteLine(
                    //" Name: " + property.Name + '\n' +
                    /*"   AllObjectTypes: " + property.AllObjectTypes + '\n' +
                    "   AutomaticValue: " + property.AutomaticValue + '\n' +
                    "   AutomaticValueType: " + property.AutomaticValueType.ToString() + '\n' +
                    "   BasedOnValueList: " + property.BasedOnValueList + '\n' +
                    "   DataType: " + property.DataType.ToString() + '\n' +*/
                    //"   ID: " + property.ID + '\n' +
                    //"   OpjectType: " + property.ObjectType + '\n' +
                    //"   ValueList: " + property.ValueList + '\n' +
                    //"--------------------------------------------");
            }
        }

        public static void ReadTypes() {
            ObjType[] objTypes = client.Get<ObjType[]>(MFRequest.ObjectTypes());
            foreach (ObjType objType in objTypes) {
                if (!Types.ContainsKey(objType.ID)) {
                    Types.Add(objType.ID, objType.Name);
                }
            }

            foreach (KeyValuePair<int, string> pair in Types) {
                Debug.Log(pair.Key + " " + pair.Value);
            }
        }

        public static void ReadTypeIcons() {
            string path;
            foreach (KeyValuePair<int, string> pair in Types) {
                Image image = Image.FromStream(client.Get<Stream>(MFRequest.ObjectTypeIcon(pair.Key)));
                path = Parser.applicationPath + "assets/textures/" + pair.Key + ".png";
                //Console.WriteLine("ReadTypeIcons: " + path);
                image.Save(path, ImageFormat.Png);
                //Console.WriteLine("ReadTypeIcons: ONE DONE");
                image.Dispose();
            }
        }

        public static MFilesObject[] GetDefaultEntryObjects(int pDefaultEntryObjectType) {
            List<MFilesObject> list = new List<MFilesObject>();
            Results<ObjectVersion> objectVersions = client.Get<Results<ObjectVersion>>(MFRequest.ObjectsOfType(pDefaultEntryObjectType));
            ObjVer objVer;
            foreach (ObjectVersion objectVersion in objectVersions.Items) {
                objVer = objectVersion.ObjVer;
                list.Add(new MFilesObject(objVer.Type, objVer.ID, objVer.Version, objectVersion.Title));
            }
            return list.ToArray();
        }

        public static MFilesObject[] GetRelationships(MFilesObject pMFilesObject, string pDirection = "") {
            List<MFilesObject> list = new List<MFilesObject>();
            ObjectVersion[] objectVersions = client.Get<ObjectVersion[]>(MFRequest.Relationships(pMFilesObject.Type, pMFilesObject.ID, pMFilesObject.Version, pDirection));
            ObjVer objVer;
            MFilesObject mFilesObject;
            foreach (ObjectVersion objectVersion in objectVersions) {
                objVer = objectVersion.ObjVer;
                mFilesObject = new MFilesObject(objVer.Type, objVer.ID, objVer.Version, objectVersion.Title);
                if (objectVersion.Files.Length > 0) {
                    mFilesObject.Title += "." + GetFiles(mFilesObject)[0].Extension;

                }
                list.Add(mFilesObject);
            }
            return list.ToArray();
        }

        public static ObjectFile[] GetFiles(MFilesObject pMFilesObject) {
            ObjectFile[] files = client.Get<ObjectFile[]>(MFRequest.ObjectFiles(pMFilesObject.Type, pMFilesObject.ID, pMFilesObject.Version));
            return files;
        }

        public static bool HasFiles(MFilesObject pMFilesObject) {
            return GetFiles(pMFilesObject).Length > 0;
        }

        public static Stream GetFileContent(MFilesObject pMFilesObject, int pFileID) {
            return client.Get<Stream>(MFRequest.Contents(pMFilesObject.Type, pMFilesObject.ID, pMFilesObject.Version, pFileID));
        }

        public static MFilesObject UploadFileContent(MFilesObject pMFilesObject, int pFileID, string pFullFilePath) {
            ObjectVersion objectVersion = client.Put<ObjectVersion>(MFRequest.Contents(
                pMFilesObject.Type, pMFilesObject.ID, pMFilesObject.Version, pFileID),
                File.Open(pFullFilePath, FileMode.Open));
            return new MFilesObject(objectVersion.ObjVer.Type, objectVersion.ObjVer.ID, objectVersion.ObjVer.Version, pMFilesObject.Title);
        }

        public static MFCheckOutStatus GetCheckOutStatus(MFilesObject pMFilesObject) {
            return client.Get<PrimitiveType<MFCheckOutStatus>>(MFRequest.CheckOutStatus(pMFilesObject.Type, pMFilesObject.ID, pMFilesObject.Version)).Value;
        }

        public static MFilesObject SetCheckOutStatus(MFilesObject pMFilesObject, MFCheckOutStatus pStatus) {
            ObjectVersion objectVersion = client.Put<ObjectVersion>(
                MFRequest.CheckOutStatus(pMFilesObject.Type, pMFilesObject.ID, pMFilesObject.Version),
                new PrimitiveType<MFCheckOutStatus>() { Value = pStatus });
            return new MFilesObject(objectVersion.ObjVer.Type, objectVersion.ObjVer.ID, objectVersion.ObjVer.Version, pMFilesObject.Title);
            //Console.WriteLine("CHoT: " + objectVersion.CheckedOutTo.ToString());
        }

        public static ObjectVersion GetObjectVersion(MFilesObject pMFilesObject) {
            return client.Get<ObjectVersion>(MFRequest.ObjectVersion(pMFilesObject.Type, pMFilesObject.ID, pMFilesObject.Version));
        }

        public static PropertyValue GetProperty(MFilesObject pMFilesObject, int pPropertyID) {
            return client.Get<PropertyValue>(MFRequest.SingleObjectProperty(pMFilesObject.Type, pMFilesObject.ID, pMFilesObject.Version, pPropertyID));
        }

        public static ExtendedObjectVersion SetProperty(MFilesObject pMFilesObject, int pPropertyID, PropertyValue pProperty) {
            return client.Put<ExtendedObjectVersion>(MFRequest.SingleObjectProperty(pMFilesObject.Type, pMFilesObject.ID, pMFilesObject.Version, pPropertyID),
                pProperty);
        }

        public static PropertyValue[] GetProperties(MFilesObject pMFilesObject) {
            return client.Get<PropertyValue[]>(
                MFRequest.ObjectProperties(pMFilesObject.Type, pMFilesObject.ID, pMFilesObject.Version));
        }

        public static PropertyDef GetPropertyDef(int pPropertyID) {
            return client.Get<PropertyDef>(MFRequest.PropertyDefinition(pPropertyID));
        }

        public static MFilesObject[] GetObjects(int pType) {
            ObjectVersion[] result = client.Get<Results<ObjectVersion>>(MFRequest.ObjectsOfType(pType)).Items;
            List<MFilesObject> mFilesObjects = new List<MFilesObject>();
            foreach (ObjectVersion objectVersion in result) {
                mFilesObjects.Add(new MFilesObject(objectVersion.ObjVer.Type, objectVersion.ObjVer.ID, objectVersion.ObjVer.Version, objectVersion.Title));
            }
            return mFilesObjects.ToArray();
        }
    }
}
