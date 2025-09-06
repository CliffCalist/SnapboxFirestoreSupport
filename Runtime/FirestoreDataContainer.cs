using Firebase.Firestore;

namespace WhiteArrow.Snapbox.FirestoreSupport
{
    [FirestoreData]
    public class FirestoreDataContainer<T>
    {
        [FirestoreProperty]
        public T Data { get; set; }
    }
}