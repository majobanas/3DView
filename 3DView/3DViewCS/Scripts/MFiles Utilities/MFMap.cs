using MFiles.MFWS;
using MFiles.MFWS.Structs;
using System.Collections.Generic;

namespace MFiles.MFWS {
    public static class MFMap {
        public static Dictionary<int, string> Class = new Dictionary<int, string>();
        public static Dictionary<int, string> Type = new Dictionary<int, string>();
        public static Dictionary<int, string> Property = new Dictionary<int, string>();
        public static Dictionary<int, string> Workflow = new Dictionary<int, string>();

        public static Dictionary<int, List<ObjectVersion>> Objects = new Dictionary<int, List<ObjectVersion>>();

        public static void Initialize(ref MFClient pClient) {
            InitializeClasses(ref pClient);
            InitializeTypes(ref pClient);
            InitializeProperties(ref pClient);
            InitializeWorkflows(ref pClient);
        }

        private static void InitializeClasses(ref MFClient pClient) {
            ObjectClass[] objectClasses = pClient.Get<ObjectClass[]>(MFRequest.ObjectClasses());
            foreach (ObjectClass objectClass in objectClasses) {
                Class.Add(objectClass.ID, objectClass.Name);
            }
        }

        private static void InitializeTypes(ref MFClient pClient) {
            ObjType[] objectTypes = pClient.Get<ObjType[]>(MFRequest.ObjectTypes());
            foreach (ObjType objectType in objectTypes) {
                Type.Add(objectType.ID, objectType.Name);
            }
        }

        private static void InitializeProperties(ref MFClient pClient) {
            PropertyDef[] propertyDefinitions = pClient.Get<PropertyDef[]>(MFRequest.PropertyDefinitions());
            foreach (PropertyDef propertyDefinition in propertyDefinitions) {
                Property.Add(propertyDefinition.ID, propertyDefinition.Name);
            }
        }

        private static void InitializeWorkflows(ref MFClient pClient) {
            Workflow[] workflows = pClient.Get<Workflow[]>(MFRequest.Workflows());
            foreach (Workflow workflow in workflows) {
                Workflow.Add(workflow.ID, workflow.Name);
            }
        }

        private static void LoadObjects(ref MFClient pClient) {
            Results<ObjectVersion> results = pClient.Get<Results<ObjectVersion>>(MFRequest.Objects());
            foreach (ObjectVersion objectVersion in results.Items) {
                if (!Objects.ContainsKey(objectVersion.ObjVer.Type)) {
                    Objects.Add(objectVersion.ObjVer.Type, new List<ObjectVersion>());
                } else {
                    Objects[objectVersion.ObjVer.Type].Add(objectVersion);
                }
            }
        }
    }
}
