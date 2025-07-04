using Firebase.Firestore;

namespace WhiteArrow.SnapboxSDK.FirestoreSupport
{
    [FirestoreData]
    public class FirestoreDataContainer<T>
    {
        [FirestoreProperty]
        public T Data { get; set; }
    }
}