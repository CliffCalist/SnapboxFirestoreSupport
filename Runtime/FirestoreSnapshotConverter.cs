using Firebase.Firestore;

namespace WhiteArrow.SnapboxSDK.FirestoreSupport
{
    public abstract class FirestoreSnapshotConverter
    {
        public abstract object ConvertFromSnapshot(DocumentSnapshot documentSnapshot);
    }

    public sealed class FirestoreSnapshotConverter<T> : FirestoreSnapshotConverter
    {
        public override object ConvertFromSnapshot(DocumentSnapshot documentSnapshot) => documentSnapshot.ConvertTo<T>();
    }
}