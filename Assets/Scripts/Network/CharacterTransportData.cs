using System;
using Unity.Netcode;

namespace Trellcko.DefenseFromMonster.GamePlay.Data
{
    [Serializable]
    public struct CharacterTransportData
    {
        public float Speed;

        public float AngularSpeed;

        public float MeleeAttackDamage;
    }

     public class CharacterTransportDataSerializer : INetworkSerializable
    {
        public CharacterTransportData myData;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref myData.AngularSpeed);
            serializer.SerializeValue(ref myData.MeleeAttackDamage);
            serializer.SerializeValue(ref myData.Speed);
        }
    }

}
