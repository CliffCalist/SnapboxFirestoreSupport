using System;
using Firebase.Firestore;

namespace WhiteArrow.SnapboxSDK.FirestoreSupport
{
    public class FirestoreSnapshotMetadata : ISnapshotMetadata
    {
        public string SnapshotName { get; private set; }
        public Type SnapshotType { get; private set; }
        public object FolderPath { get; private set; }
        public CollectionReference CastedFolderPath { get; private set; }

        public bool IsChanged { get; set; }
        public bool IsDeleted { get; set; }

        public FirestoreSnapshotMetadata(string dataName, Type dataType, CollectionReference folderPath)
        {
            if (string.IsNullOrWhiteSpace(dataName))
                throw new ArgumentException(nameof(dataName));
            SnapshotName = dataName;

            SnapshotType = dataType ?? throw new ArgumentNullException(nameof(dataType), "The snapshot type cannot be null.");

            if (folderPath == null)
                throw new ArgumentNullException("The provided path is null or empty.", nameof(folderPath));

            CastedFolderPath = folderPath;
            FolderPath = CastedFolderPath;
        }
    }
}
