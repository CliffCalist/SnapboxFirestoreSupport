using System;
using Firebase.Firestore;

namespace WhiteArrow.SnapboxSDK.FirestoreSupport
{
    public class FirestoreSnapshotMetadata : ISnapshotMetadata
    {
        public string SnapshotName { get; private set; }
        public Type SnapshotType { get; private set; }
        public FirestoreSnapshotConverter Converter { get; private set; }

        public object FolderPath { get; private set; }
        public CollectionReference CastedFolderPath { get; private set; }

        public bool IsChanged { get; set; }
        public bool IsDeleted { get; set; }

        public FirestoreSnapshotMetadata(string dataName, FirestoreSnapshotConverter snapshotConverter, CollectionReference folderPath)
        {
            if (string.IsNullOrWhiteSpace(dataName))
                throw new ArgumentException(nameof(dataName));
            SnapshotName = dataName;

            Converter = snapshotConverter ?? throw new ArgumentNullException(nameof(snapshotConverter));

            if (folderPath == null)
                throw new ArgumentNullException("The provided path is null or empty.", nameof(folderPath));

            CastedFolderPath = folderPath;
            FolderPath = CastedFolderPath;
        }
    }
}
