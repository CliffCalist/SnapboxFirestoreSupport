using System;
using Firebase.Firestore;

namespace WhiteArrow.DataSaving
{
    public class FirestoreSavingMetadata : ISavingMetadata
    {
        public string DataName { get; private set; }
        public Type DataType { get; private set; }
        public object FolderPath { get; private set; }
        public CollectionReference CastedFolderPath { get; private set; }

        public bool IsChanged { get; set; }
        public bool IsDeleted { get; set; }

        public FirestoreSavingMetadata(string dataName, Type dataType, CollectionReference folderPath)
        {
            if (string.IsNullOrWhiteSpace(dataName))
                throw new ArgumentException(nameof(dataName));
            DataName = dataName;

            DataType = dataType ?? throw new ArgumentNullException(nameof(dataType), "Data type cannot be null.");

            if (folderPath == null)
                throw new ArgumentNullException("The provided path is null. oe empty", nameof(folderPath));

            CastedFolderPath = folderPath;
            FolderPath = CastedFolderPath;
        }
    }
}
