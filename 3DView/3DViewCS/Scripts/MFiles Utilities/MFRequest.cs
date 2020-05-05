namespace MFiles.MFWS {
    public static class MFRequest {
        /// <summary>
        /// POST | Transient resource used in calculating new authentication tokens.
        /// </summary>
        public static string AuthenticationTokens() {
            return "/server/authenticationtokens";
        }
        /// <summary>
        /// GET | PUT | Resource representing the check out state of the object.
        /// </summary>
        public static string CheckOutStatus(int pType, int pObjectID, int pVersion) {
            return string.Format("/objects/{0}/{1}/{2}/checkedout", pType, pObjectID, pVersion);
        }
        /// <summary>
        /// GET | Class Icon.
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        public static string ClassIcon(int pID) {
            return string.Format("/structure/classes/{0}/icon", pID);
        }
        /// <summary>
        /// GET | PUT | Resource listing the full object comment history.
        /// </summary>
        public static string Comments(int pType, int pObjectID, int pVersion) {
            return string.Format("/objects/{0}/{1}/{2}/comments", pType, pObjectID, pVersion);
        }
        /// <summary>
        /// GET | PUT | The contents of a single file.
        /// </summary>
        public static string Contents(int pType, int pObjectID, int pVersion, int pFileID) {
            return string.Format("/objects/{0}/{1}/{2}/files/{3}/content", pType, pObjectID, pVersion, pFileID);
        }
        /// <summary>
        /// GET | PUT | Resource representing the Deleted-state of an object.
        /// </summary>
        public static string Deleted(int pType, int pObjectID) {
            return string.Format("/objects/{0}/{1}/deleted", pType, pObjectID);
        }
        /// <summary>
        /// PUT | The refresh status for an external object type.
        /// </summary>
        public static string ExternalSourceRefresh(int pType) {
            return string.Format("/structure/objecttypes/{0}/refreshstatus", pType);
        }
        /// <summary>
        /// GET | DELETE | A single favorite object.
        /// </summary>
        public static string FavoriteObject(int pType, int pObjectID) {
            return string.Format("/favorites/{0}/{1}", pType, pObjectID);
        }
        /// <summary>
        /// GET | POST | Collection of favorited objects.
        /// </summary>
        public static string Favorites() {
            return "/favorites";
        }
        /// <summary>
        /// GET | PUT | The file name of an object file.
        /// </summary>
        public static string FileName(int pType, int pObjectID, int pVersion, int pFileID) {
            return string.Format("/objects/{0}/{1}/{2}/files/{3}/title", pType, pObjectID, pVersion, pFileID);
        }
        /// <summary>
        /// GET | File thumbnail.
        /// </summary>
        public static string FileThumbnail(int pType, int pObjectID, int pVersion, int pFileID) {
            return string.Format("/objects/{0}/{1}/{2}/files/{3}/preview", pType, pObjectID, pVersion, pFileID);
        }
        /// <summary>
        /// GET | Resource listing the full object version history.
        /// </summary>
        public static string History(int pType, int pObjectID) {
            return string.Format("/objects/{0}/{1}/history", pType, pObjectID);
        }
        /// <summary>
        /// A single object.
        /// </summary>
        public static string Object(int pType, int pObjectID) {
            return string.Format("/objects/{0}/{1}", pType, pObjectID);
        }
        /// <summary>
        /// GET | Information on a single object class.
        /// </summary>
        public static string ObjectClass(int pID) {
            return string.Format("/structure/classes/{0}", pID);
        }
        /// <summary>
        /// GET | Collection of object class information.
        /// </summary>
        public static string ObjectClasses() {
            return "/structure/classes";
        }
        /// <summary>
        /// GET | DELETE | A single file on an object.
        /// </summary>
        public static string ObjectFile(int pType, int pObjectID, int pVersion, int pFileID) {
            return string.Format("/objects/{0}/{1}/{2}/files/{3}", pType, pObjectID, pVersion, pFileID);
        }
        /// <summary>
        /// GET | PUT | Files belonging to an object.
        /// </summary>
        public static string ObjectFiles(int pType, int pObjectID, int pVersion) {
            return string.Format("/objects/{0}/{1}/{2}/files", pType, pObjectID, pVersion);
        }
        /// <summary>
        /// GET | POST | PUT | The properties of an object.
        /// </summary>
        public static string ObjectProperties(int pType, int pObjectID, int pVersion) {
            return string.Format("/objects/{0}/{1}/{2}/properties", pType, pObjectID, pVersion);
        }
        /// <summary>
        /// GET | The object thumbnail.
        /// </summary>
        public static string ObjectThumbnail(int pType, int pObjectID, int pVersion) {
            return string.Format("/objects/{0}/{1}/{2}/preview", pType, pObjectID, pVersion);
        }
        /// <summary>
        /// GET | PUT | Resource representing the object title.
        /// </summary>
        public static string ObjectTitle(int pType, int pObjectID, int pVersion) {
            return string.Format("/objects/{0}/{1}/{2}/title", pType, pObjectID, pVersion);
        }
        /// <summary>
        /// GET | Information on a single object type.
        /// </summary>
        public static string ObjectType(int pType) {
            return string.Format("/structure/objecttypes/{0}", pType);
        }
        /// <summary>
        /// GET | Object type icon.
        /// </summary>
        public static string ObjectTypeIcon(int pType, int pSize = 64) {
            return string.Format("/structure/objecttypes/{0}/icon?size={1}", pType, pSize);
        }
        /// <summary>
        /// GET | Collection of object type information.
        /// </summary>
        public static string ObjectTypes() {
            return string.Format("/structure/objecttypes");
        }
        /// <summary>
        /// GET | DELETE | A single object version.
        /// </summary>
        public static string ObjectVersion(int pType, int pObjectID, int pVersion) {
            return string.Format("/objects/{0}/{1}/{2}", pType, pObjectID, pVersion);
        }
        /// <summary>
        /// GET | PUT | Resource representing the object workflow state.
        /// </summary>
        public static string ObjectWorkflowState(int pType, int pObjectID, int pVersion) {
            return string.Format("/objects/{0}/{1}/{2}/workflowstate", pType, pObjectID, pVersion);
        }
        /// <summary>
        /// GET | Collection of objects in the document vault.
        /// </summary>
        public static string Objects() {
            return "/objects";
        }
        /// <summary>
        /// GET | POST | Collection of objects filtered by object type.
        /// </summary>
        public static string ObjectsOfType(int pType) {
            return string.Format("/objects/{0}", pType);
        }
        /// <summary>
        /// GET | POST | A resource that allows access to properties of multiple objects.
        /// </summary>
        public static string PropertiesOfMultipleObjects() {
            return "/objects/properties";
        }
        /// <summary>
        /// GET | Information on a single property definition.
        /// </summary>
        public static string PropertyDefinition(int pID) {
            return string.Format("/structure/properties/{0}", pID);
        }
        /// <summary>
        /// GET | Collection of property definitions.
        /// </summary>
        public static string PropertyDefinitions() {
            return "/structure/properties";
        }
        /// <summary>
        /// GET | POST | A collection of objects recently accessed by the current user.
        /// </summary>
        public static string RecentryAccessedByTheUser() {
            return "/recentlyaccessedbyme";
        }
        /// <summary>
        /// GET | The count of the related objects.
        /// </summary>
        public static string RelationshipCount(int pType, int pObjectID, int pVersion) {
            return string.Format("/objects/{0}/{1}/{2}/relationships/count", pType, pObjectID, pVersion);
        }
        /// <summary>
        /// GET | A collection of related objects.
        /// </summary>
        public static string Relationships(int pType, int pObjectID, int pVersion, string pDirection = "") {
            if (pDirection != "") {
                pDirection = "?direction=" + pDirection;
            }
            return string.Format("/objects/{0}/{1}/{2}/relationships{3}", pType, pObjectID, pVersion, pDirection);
        }
        /// <summary>
        /// GET | All objects found within the view.
        /// </summary>
        public static string SearchViewContents(string pPath) {
            return string.Format("/views/{0}/objects", pPath);
        }
        /// <summary>
        /// GET | The server public key used to secure messages between the client and the server.
        /// </summary>
        public static string ServerPublicKey() {
            return "/server/publickey";
        }
        /// <summary>
        /// Server-level resources.
        /// </summary>
        public static string ServerResources() {
            return "/server";
        }
        /// <summary>
        /// GET | Server status.
        /// </summary>
        public static string ServerStatus() {
            return "/server/status";
        }
        /// <summary>
        /// GET | PUT | DELETE | Current user session information.
        /// </summary>
        public static string Session() {
            return "/session";
        }
        /// <summary>
        /// GET | PUT | DELETE | A single property of an object.
        /// </summary>
        public static string SingleObjectProperty(int pType, int pObjectID, int pVersion, int pID) {
            return string.Format("/objects/{0}/{1}/{2}/properties/{3}", pType, pObjectID, pVersion, pID);
        }
        /// <summary>
        /// GET | The count of the sub-objects.
        /// </summary>
        public static string SubObjectCount(int pType, int pObjectID, int pVersion) {
            return string.Format("/objects/{0}/{1}/{2}/subobjects/count", pType, pObjectID, pVersion);
        }
        /// <summary>
        /// GET | A collection of sub-objects.
        /// </summary>
        public static string SubObjects(int pType, int pObjectID, int pVersion) {
            return string.Format("/objects/{0}/{1}/{2}/subobjects", pType, pObjectID, pVersion);
        }
        /// <summary>
        /// POST | A collection of temporary uploads.
        /// </summary>
        public static string TemporaryUpload() {
            return "/files";
        }
        /// <summary>
        /// GET | DELETE | Single value list item information.
        /// </summary>
        public static string ValueListItem(int pID, int pObjectID) {
            return string.Format("/valuelists/{0}/items/{1}", pID, pObjectID);
        }
        /// <summary>
        /// GET | PUT | The title of a value list item.
        /// </summary>
        public static string ValueListItemTitle(int pID, int pObjectID) {
            return string.Format("/valuelists/{0}/items/{1}/title", pID, pObjectID);
        }
        /// <summary>
        /// GET | POST | Collection of value list items for a single value list.
        /// </summary>
        public static string ValueListItems(int pID) {
            return string.Format("/valuelists/{0}/items", pID);
        }
        /// <summary>
        /// GET | PUT | The document vault attached to the current session.
        /// </summary>
        public static string Vault() {
            return "/session/vault";
        }
        /// <summary>
        /// Vault metadata structure information.
        /// </summary>
        public static string VaultStructure() {
            return "/structure";
        }
        /// <summary>
        /// GET | Collection of vaults on the server.
        /// </summary>
        public static string Vaults() {
            return "/server/vaults";
        }
        /// <summary>
        /// A single view in the view hierarchy.
        /// </summary>
        public static string View(string pPath) {
            return string.Format("/views/{0}", pPath);
        }
        /// <summary>
        /// GET | Contents of a single view.
        /// </summary>
        public static string ViewContents(string pPath) {
            return string.Format("/views/{0}/items", pPath);
        }
        /// <summary>
        /// GET | The count of items within the single view.
        /// </summary>
        public static string ViewContentsCount(string pPath) {
            return string.Format("/views/{0}/items/count", pPath);
        }
        /// <summary>
        /// GET | Information on a single workflow.
        /// </summary>
        public static string Workflow(int pID) {
            return string.Format("/structure/workflows/{0}", pID);
        }
        /// <summary>
        /// GET | Information on a single workflow state.
        /// </summary>
        public static string WorkflowState(int pID) {
            return string.Format("/structure/workflows/{0}/states/{0}", pID);
        }
        /// <summary>
        /// GET | Collection of states under a single workflow.
        /// </summary>
        public static string WorkflowStates(int pID) {
            return string.Format("/structure/workflows/{0}/states", pID);
        }
        /// <summary>
        /// GET | Collection of workflows.
        /// </summary>
        public static string Workflows() {
            return "/structure/workflows";
        }

    }
}
