using System;
using System.Threading.Tasks;

namespace WhiteArrow.SnapboxSDK.FirestoreSupport
{
    public class FirestoreSnapshotSaver : ISnapshotSaver
    {
        public void Save(ISnapshotMetadata metadata, object data)
        {
            throw new NotSupportedException("Firestore only supports asynchronous saving. Use SaveAsync instead.");
        }

        public async Task SaveAsync(ISnapshotMetadata metadata, object data)
        {
            try
            {
                if (metadata is not FirestoreSnapshotMetadata castedMetadata)
                    throw new InvalidOperationException($"Expected metadata of type {nameof(FirestoreSnapshotMetadata)}, but received {metadata.GetType()}");

                var docRef = castedMetadata.CastedFolderPath.Document(metadata.SnapshotName);
                await docRef.SetAsync(data);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error while saving snapshot to Firestore: {ex.Message}", ex);
            }
        }



        public void Delete(ISnapshotMetadata metadata)
        {
            throw new NotSupportedException("Firestore only supports asynchronous deletion. Use DeleteAsync instead.");
        }

        public async Task DeleteAsync(ISnapshotMetadata metadata)
        {
            try
            {
                if (metadata is not FirestoreSnapshotMetadata castedMetadata)
                    throw new InvalidOperationException($"Expected metadata of type {nameof(FirestoreSnapshotMetadata)}, but received {metadata.GetType()}");

                var docRef = castedMetadata.CastedFolderPath.Document(metadata.SnapshotName);
                await docRef.DeleteAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error while deleting snapshot in Firestore: {ex.Message}", ex);
            }
        }
    }
}
