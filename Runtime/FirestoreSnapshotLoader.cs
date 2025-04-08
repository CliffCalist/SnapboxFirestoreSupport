using System;
using System.IO;
using System.Threading.Tasks;
using Firebase.Firestore;

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
                {
                    var convertToMethod = typeof(DocumentSnapshot).GetMethod("ConvertTo", new Type[] { });

                    if (convertToMethod == null)
                        throw new InvalidOperationException("ConvertTo method not found.");

                    var data = convertToMethod.MakeGenericMethod(metadata.SnapshotType).Invoke(snapshot, null);

                    if (data == null)
                        throw new InvalidOperationException("Failed to deserialize the snapshot from Firestore.");

                    return data;
                }
                else throw new FileNotFoundException($"No snapshot found at {castedMetadata.CastedFolderPath} in Firestore.");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error while loading snapshot from Firestore: {ex.Message}", ex);
            }
        }
    }
}
