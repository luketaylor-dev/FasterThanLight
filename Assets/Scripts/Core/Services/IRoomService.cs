using UnityEngine;
using FTL.Core.Events;

namespace FTL.Core.Services
{
    public interface IRoomService
    {
        RoomComponent CreateRoom(RoomType roomType, ShipComponent ship, Vector3 position);
        void DestroyRoom(RoomComponent room);
        bool IsRoomAlive(RoomComponent room);
        float GetRoomHealth(RoomComponent room);
        void DamageRoom(RoomComponent room, float damage);
        RoomType GetRoomType(RoomComponent room);
        ShipComponent GetRoomShip(RoomComponent room);
        Vector3 GetRoomPosition(RoomComponent room);
        void SetRoomPosition(RoomComponent room, Vector3 position);
    }
}
