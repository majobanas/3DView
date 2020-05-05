
namespace ThreeDViewCS {
    public class MFilesObject {

        public int Type;
        public int ID;
        public int Version;
        public string Title;

        public MFilesObject( int pType, int pID, int pVersion, string pTitle) {
            Type = pType;
            ID = pID;
            Version = pVersion;
            Title = Parser.UnicodeToAscii(pTitle);
        }

        public override string ToString() {
            return Type + Parser.itemSplitter + ID + Parser.itemSplitter + Version + Parser.itemSplitter + Title;
        }
    }
}
