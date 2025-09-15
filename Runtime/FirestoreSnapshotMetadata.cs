using System;
using Firebase.Firestore;

namespace WhiteArrow.Snapbox.FirestoreSupport
{
    public class FirestoreSnapshotMetadata : ISnapshotMetadata
    {
        public string SnapshotName { get; private set; }
        public Type SnapshotType { get; private set; }

        public object FolderPath => CastedFolderPath;
        public CollectionReference CastedFolderPath { get; private set; }

        public bool IsChanged { get; set; }
        public bool IsDeleted { get; set; }



        public FirestoreSnapshotMetadata(string dataName, Type snapshotType, CollectionReference folderPath)
        {
            if (string.IsNullOrWhiteSpace(dataName))
                throw new ArgumentException(nameof(dataName));

            SnapshotName = dataName;
            SnapshotType = snapshotType ?? throw new ArgumentNullException(nameof(snapshotType));
            CastedFolderPath = folderPath ?? throw new ArgumentNullException(nameof(folderPath));
        }
    }
}
