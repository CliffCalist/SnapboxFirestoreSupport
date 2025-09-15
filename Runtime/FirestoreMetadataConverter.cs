using System;
using Firebase.Firestore;
using UnityEngine;

namespace WhiteArrow.Snapbox.FirestoreSupport
{
    public class FirestoreMetadataConverter : ISnapshotMetadataConverter
    {
        private FirebaseFirestore _firestore => FirebaseFirestore.DefaultInstance;


        public ISnapshotMetadata Convert(SnapshotMetadataDescriptor descriptor)
        {
            CollectionReference collectionRef = null;
            var parts = descriptor.Path.Split('/', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length % 2 == 0)
            {
                var fixedPath = descriptor.Path + "/__snapbox_auto_collection";
                collectionRef = _firestore.Collection(fixedPath);

                Debug.LogWarning(
                    $"[Snapbox][Firestore] Path '{descriptor.Path}' ended on a document. " +
                    $"Automatically appended '__snapbox_auto_collection'. Final path: '{fixedPath}'.");
            }
            else collectionRef = _firestore.Collection(descriptor.Path);

            return new FirestoreSnapshotMetadata(
                descriptor.Name,
                descriptor.SnapshotType,
                collectionRef
            );
        }
    }
}