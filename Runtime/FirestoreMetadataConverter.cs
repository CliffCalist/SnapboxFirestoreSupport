using System;
using Firebase.Firestore;

namespace WhiteArrow.Snapbox.FirestoreSupport
{
    public class FirestoreMetadataConverter : ISnapshotMetadataConverter
    {
        private readonly Func<string> _rootPathProvider;
        private readonly FirebaseFirestore _firestore;



        private const string AUTO_COLLECTION_NAME = "snapbox_auto_collection";



        public FirestoreMetadataConverter(Func<string> rootPathProvider = null)
        {
            _firestore = FirebaseFirestore.DefaultInstance;
            _rootPathProvider = rootPathProvider ?? (() => string.Empty);
        }


        public ISnapshotMetadata Convert(SnapshotMetadataDescriptor descriptor)
        {
            var rootPath = _rootPathProvider?.Invoke() ?? string.Empty;
            var fullPath = CombinePaths(rootPath, descriptor.Path);

            var validatedPath = ValidateCollectionPath(fullPath);
            var collectionRef = _firestore.Collection(validatedPath);

            return new FirestoreSnapshotMetadata(
                descriptor.Name,
                descriptor.SnapshotType,
                collectionRef
            );
        }

        private string CombinePaths(string rootPath, string descriptorPath)
        {
            if (string.IsNullOrWhiteSpace(rootPath) && string.IsNullOrWhiteSpace(descriptorPath))
                return string.Empty;

            if (string.IsNullOrWhiteSpace(rootPath))
                return descriptorPath;

            if (string.IsNullOrWhiteSpace(descriptorPath))
                return rootPath;

            return $"{rootPath.TrimEnd('/')}/{descriptorPath.TrimStart('/')}";
        }

        private string ValidateCollectionPath(string path)
        {
            var parts = path.Split('/', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length % 2 == 0)
                return path + "/" + AUTO_COLLECTION_NAME;

            return path;
        }
    }
}