﻿using System;
using MFiles.MFWS.Structs;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ThreeDViewCS {
    public static class Parser {
        public const char objectSplitter = '%';
        public const char itemSplitter = '?';

        public static string applicationPath;
        public static string entryObjects;
        public static string vaultGUID;

        public static void Initialize(string pApplicationPath, string pEntryObjects, string pVaultGUID) {
            applicationPath = ApplicationPath(pApplicationPath);
            entryObjects = pEntryObjects;
            vaultGUID = VaultGUID(pVaultGUID);
        }

        public static string[] InputArgs(string[] pArgs) {
            string[] retrunArgs = new string[] { "NULL", "NULL", "NULL"};
            if (pArgs.Length != 0) {
                retrunArgs = pArgs[0].Split('%');
            }
            //Console.WriteLine("applicationPath: " + retrunArgs[0]);
            //Console.WriteLine("entryObjects: " + retrunArgs[1]);
            //Console.WriteLine("vaultGUID: " + retrunArgs[2]);
            return retrunArgs;
        }

        private static string ApplicationPath(string pApplicationPath) {
            string result = (pApplicationPath == "NULL" ? "../" : pApplicationPath);
            Debug.Log("C# Parser.ApplicationPath: " + result);
            return result;
        }
        private static string VaultGUID(string pVaultGUID) {
            //string result;
            if (pVaultGUID == "NULL") {
                //{4B435E09-EB5E-4B65-8900-1551D8A3CB73}
                return "{72B08B17-FE4E-4DDB-92D9-77624FA1FC6E}";
            } else {
                return pVaultGUID;
            }
        }

        public static MFilesObject[] MFilesObject(string pObjectTypeIDVersionTitle) {
            List<MFilesObject> result = new List<MFilesObject>();
            if (pObjectTypeIDVersionTitle != "NULL") {
                string[] objects = pObjectTypeIDVersionTitle.Split(objectSplitter);
                //Console.WriteLine(objects.Length + pObjectTypeIDVersionTitle);
                foreach (string obj in objects) {
                    string[] typeIDVersionTitle = obj.Split(itemSplitter);
                    //Console.WriteLine(typeIDVersionTitle.Length + obj);
                    result.Add(new MFilesObject(
                        int.Parse(typeIDVersionTitle[0]),
                        int.Parse(typeIDVersionTitle[1]),
                        int.Parse(typeIDVersionTitle[2]),
                        typeIDVersionTitle[3]
                    ));
                }
                
            }
            if (result.Count == 0) return null;
            //Console.WriteLine("C# Parser.MFilesObject: " + ((result != null) ? result.Type + " " + result.ID + " " + result.Version : "NULL"));
            return result.ToArray();
        }

        public static string UnicodeToAscii(string pString) {
            string result = string.Concat(pString.Normalize(System.Text.NormalizationForm.FormD).Where(
                c => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark));
            return result;
        }

        public static MFilesObject[] EntryObjects(string pEntryObjects) {
            List<MFilesObject> list = new List<MFilesObject>();
            if (pEntryObjects != "NULL") {
                string[] entryObjects = pEntryObjects.Split(objectSplitter);
                MFilesObject mFilesObject;
                foreach (string entryObject in entryObjects) {
                    string[] typeIDVersion = entryObject.Split(itemSplitter);
                    mFilesObject = new MFilesObject(
                        int.Parse(typeIDVersion[0]),
                        int.Parse(typeIDVersion[1]),
                        int.Parse(typeIDVersion[2]),
                        "TO BE ASSIGNED"
                    );
                    ObjectVersion objectVersion = MFiles.GetObjectVersion(mFilesObject);
                    mFilesObject.Title = objectVersion.Title;
                    if (objectVersion.Files.Length > 0) {
                        string extension = "." + MFiles.GetFiles(mFilesObject)[0].Extension;
                        if (!mFilesObject.Title.Contains(extension)) {
                            mFilesObject.Title += extension;
                        }

                    }
                    list.Add(mFilesObject);
                }
            }
            Debug.Log("C# Parser.EntryObjects: " + list.Count + " entryObjects");
            return list.ToArray();
        }

        public static byte[] StreamToBytes(Stream pStream) {
            byte[] bytes;
            if (pStream is MemoryStream)
                return ((MemoryStream)pStream).ToArray();
            MemoryStream memoryStream = new MemoryStream();
            pStream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
public static class Debug {

    public static bool log = false;
    public static bool error = true;
    public static void Log(string pMessage) {
        if (log)
            Console.WriteLine(pMessage);
    }
    public static void Error(string pMessage) {
        if (error)
            Console.WriteLine(pMessage);
    }
}
