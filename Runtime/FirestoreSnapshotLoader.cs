using System;
using System.Threading.Tasks;

namespace WhiteArrow.SnapboxSDK.FirestoreSupport
{
    public class FirestoreSnapshotLoader : ISnapshotLoader
    {
        public async Task<object> LoadAsync(ISnapshotMetadata metadata)
        {
            try
            {
                if (metadata is not FirestoreSnapshotMetadata castedMetadata)
                    throw new InvalidOperationException($"Expected metadata of type {nameof(FirestoreSnapshotMetadata)}, but received {metadata.GetType()}");

                var docRef = castedMetadata.CastedFolderPath.Document(castedMetadata.SnapshotName);
                var snapshot = await docRef.GetSnapshotAsync();

                if (snapshot.Exists)
                    return castedMetadata.Converter.ConvertFromSnapshot(snapshot);
                else return null;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error while loading snapshot from Firestore: {ex.Message}", ex);
            }
        }
    }
}
