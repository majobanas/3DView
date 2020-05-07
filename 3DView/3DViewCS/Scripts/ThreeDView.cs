using System;
using System.Collections.Generic;
using MFiles.MFWS.Structs;
using ThreeDViewCLR;
using System.Diagnostics;

namespace ThreeDViewCS {
    public class ThreeDView : ThreeDViewWrapper {

        public bool Initialized { get; private set; }
        public bool IsInFocus { get; set; }

        public MFilesObject RootObject = null;
        public MFilesObject SelectedObject = null;

        private Dictionary<int, bool> CreationFilterType = new Dictionary<int, bool>();
        private Dictionary<int, bool> VisibilityFilterType = new Dictionary<int, bool>();

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
                SelectedObject = mFilesObjects[i];
                RootObject = SelectedObject;
                AddRoot(SelectedObject);
                // Add From relationships
                AddFrom(SelectedObject);
                // Add To relationships
                AddTo(SelectedObject);
            }
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

        public MFilesObject SelectObject() {
            return Parser.MFilesObject(getObjectTypeIDVersion());
        }

        public bool IsRootSelected() {
            if (SelectedObject == null || RootObject == null)
                return false;
            return (SelectedObject.Type == RootObject.Type &&
                SelectedObject.ID == RootObject.ID &&
                SelectedObject.Version == RootObject.Version);
            }

        public void AddRoot(MFilesObject pRoot) {
            addRoot(pRoot.Type, pRoot.ID, pRoot.Version, pRoot.Title, "project_model");
        }

        public void MakeRoot() {
            if (SelectedObject != RootObject) {
                RootObject = SelectedObject;
                makeRoot();
                AddRoot(RootObject);
                AddFrom(SelectedObject);
                AddTo(SelectedObject);
            }
        }

        public void AddFrom(MFilesObject pSelectedObject) {
            MFilesObject[] froms = MFiles.GetRelationships(pSelectedObject, "from");
            for (int i = 0; i < froms.Length; i++) {
                if (CreationFilterType[froms[i].Type]) {

                    addFrom(froms[i].Type, froms[i].ID, froms[i].Version, froms[i].Title, "from_model");
                }
            }
        }

        public void AddTo(MFilesObject pSelectedObject) {
            MFilesObject[] tos = MFiles.GetRelationships(pSelectedObject, "to");
            for (int i = 0; i < tos.Length; i++) {
                if (CreationFilterType[tos[i].Type]) {
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
            toggleRender(pType, pBool);
        }
    }
}
