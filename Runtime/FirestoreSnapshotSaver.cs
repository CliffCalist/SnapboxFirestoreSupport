using System;
using System.Threading.Tasks;

namespace WhiteArrow.SnapboxSDK.FirestoreSupport
{
    public class FirestoreSnapshotSaver : ISnapshotSaver
    {
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
