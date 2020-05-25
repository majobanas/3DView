using System;
using System.Collections.Generic;
using MFiles.MFWS.Structs;
using ThreeDViewCLR;
using System.Diagnostics;

namespace ThreeDViewCS {
    public class ThreeDView : ThreeDViewWrapper {

        public bool Initialized { get; private set; }
        public bool IsInFocus { get; set; }

        public List<MFilesObject> DownloadedObjects = new List<MFilesObject>();

        public MFilesObject RootObject = null;
        public MFilesObject[] SelectedObject = null;

        private Dictionary<int, bool> CreationFilterType = new Dictionary<int, bool>();
        private Dictionary<int, bool> VisibilityFilterType = new Dictionary<int, bool>();
        public Dictionary<int, bool> PropertiesFilter = new Dictionary<int, bool>();
        

        public ThreeDView(string pApplicationPath) : base(pApplicationPath) {
        }

        public void Run() {
            if (Initialized) {
                if (IsInFocus) {
                    if (timeToInput()) {
                        processInput();
                    }
                    if (timeToUpdate()) {
                        processUpdate();
                    }
                    if (timeToRender()) {
                        processRender();
                    }
                    /*if (timeToSecond()) {
                        processSecond();
                    }*/
                }
            }
        }

        public void InitializeView(IntPtr pHandle) {
            initializeView(pHandle);
            foreach (KeyValuePair<int, string> pair in MFiles.Types) {
                addTypeToLineStatus(pair.Key);
            }
            Initialized = true;
            IsInFocus = true;
        }

        public void LoadEntryObjects(string pEntryObjects) {
            MFilesObject[] mFilesObjects = Parser.EntryObjects(pEntryObjects);
            if (mFilesObjects.Length == 0) {
                mFilesObjects = MFiles.GetDefaultEntryObjects(getConfigInteger("default_entry_object_type"));
            }
            for (int i = 0; i < 1; i++) {
                Debug.Log("C# ThreeDView.LoadEntryObjects " + mFilesObjects[i].Type + Parser.itemSplitter + mFilesObjects[i].ID + Parser.itemSplitter + mFilesObjects[i].Version + Parser.itemSplitter + mFilesObjects[i].Title);
                // Add Root (Becomes selectedObject)
                SelectedObject = mFilesObjects;
                RootObject = SelectedObject[0];
                AddRoot(SelectedObject[0]);
                // Add From relationships
                AddFrom(SelectedObject[0]);
                // Add To relationships
                AddTo(SelectedObject[0]);
            }
            SelectedObject = new MFilesObject[] { RootObject };
        }

        public void SetConfigString(string pKey, string pValue) {
            setConfigString(pKey, pValue);
        }

        public void SetConfigInteger(string pKey, int pValue) {
            setConfigInteger(pKey, pValue);
        }

        public void SetConfigFloat(string pKey, float pValue) {
            setConfigFloat(pKey, pValue);
        }
        public string GetConfigString(string pKey) {
            return getConfigString(pKey);
        }

        public int GetConfigInteger(string pKey) {
            return getConfigInteger(pKey);
        }

        public float GetConfigFloat(string pKey) {
            return getConfigFloat(pKey);
        }

        public MFilesObject[] SelectObject() {
            SelectedObject = null;
            return Parser.MFilesObject(getObjectTypeIDVersion());
        }

        public bool IsRootSelected() {
            if (SelectedObject == null || RootObject == null)
                return false;
            return (SelectedObject[0].Type == RootObject.Type &&
                SelectedObject[0].ID == RootObject.ID &&
                SelectedObject[0].Version == RootObject.Version);
            }

        public void AddRoot(MFilesObject pRoot) {
            DownloadedObjects.Add(new MFilesObject(pRoot.Type, pRoot.ID, pRoot.Version, pRoot.Title));
            addRoot(pRoot.Type, pRoot.ID, pRoot.Version, pRoot.Title, "project_model");
        }

        public void MakeRoot() {
            if (SelectedObject[0] != RootObject) {
                RootObject = SelectedObject[0];
                DownloadedObjects.Clear();
                makeRoot();
                AddRoot(RootObject);
                AddFrom(SelectedObject[0]);
                AddTo(SelectedObject[0]);
            }
        }

        public void AddFrom(MFilesObject pSelectedObject) {
            MFilesObject[] froms = MFiles.GetRelationships(pSelectedObject, "from");
            for (int i = 0; i < froms.Length; i++) {
                if (CreationFilterType[froms[i].Type]) {
                    DownloadedObjects.Add(new MFilesObject(froms[i].Type, froms[i].ID, froms[i].Version, froms[i].Title));
                    addFrom(froms[i].Type, froms[i].ID, froms[i].Version, froms[i].Title, "from_model");
                }
            }
        }

        public void AddTo(MFilesObject pSelectedObject) {
            MFilesObject[] tos = MFiles.GetRelationships(pSelectedObject, "to");
            for (int i = 0; i < tos.Length; i++) {
                if (CreationFilterType[tos[i].Type]) {
                    DownloadedObjects.Add(new MFilesObject(tos[i].Type, tos[i].ID, tos[i].Version, tos[i].Title));
                    addTo(tos[i].Type, tos[i].ID, tos[i].Version, tos[i].Title, "to_model");
                }
            }
        }

        public void RemoveFrom() {
            removeFrom();
        }

        public void RemoveTo() {
            removeTo();
        }

        public void CycleSkybox() {
            cycleSkybox();
        }

        public void InitializeCreationFilterTypes() {
            foreach (KeyValuePair<int, string> pair in MFiles.Types) {
                CreationFilterType.Add(pair.Key, true);
            }
        }

        public void ToggleCreationFilterType(int pType) {
            if (CreationFilterType.ContainsKey(pType)) {
                CreationFilterType[pType] = !CreationFilterType[pType];
            } else {
                CreationFilterType.Add(pType, false);
            }
        }

        public void InitializeVisibilityFilterTypes() {
            foreach (KeyValuePair<int, string> pair in MFiles.Types) {
                VisibilityFilterType.Add(pair.Key, true);
            }
        }
        public void ToggleVisibilityFilterType(int pType, bool pBool) {
            VisibilityFilterType[pType] = pBool;
            toggleRender(pType, pBool);
        }

        public void InitializePropertiesFilter() {
            foreach (KeyValuePair<int, string> pair in MFiles.Properties) {
                PropertiesFilter.Add(pair.Key, true);
            }
        }

        public void TogglePropertiesFilter(int pID, bool pBool) {
            if (PropertiesFilter.ContainsKey(pID)) {
                PropertiesFilter[pID] = pBool;
            } else {
                PropertiesFilter.Add(pID, pBool);
            }
        }

        public void ToggleRender(int pType, int pID, bool pBool) {
            if (VisibilityFilterType[pType])
                toggleRender(pType, pID, pBool);
        }

        public void SetDefaultView() {
            setDefaultView();
        }

    }
}
