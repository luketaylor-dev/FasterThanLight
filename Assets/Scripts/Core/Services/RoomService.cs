using UnityEngine;
using FTL.Core.Events;
using FTL.Core.Components;

namespace FTL.Core.Services
{
    public class RoomService : IRoomService
    {
        private const float DEFAULT_ROOM_HEALTH = 50f;

        public RoomComponent CreateRoom(RoomType roomType, ShipComponent ship, Vector3 position)
        {
            var room = new GameObject($"{roomType}Room");
            room.transform.position = position;
            room.transform.SetParent(ship.transform);

            var roomComponent = room.AddComponent<RoomComponent>();
            roomComponent.Initialize(roomType, DEFAULT_ROOM_HEALTH);

            return roomComponent;
        }

        public void DestroyRoom(RoomComponent room)
        {
            if (room == null) return;

            CombatEvents.OnRoomDestroyed(room.gameObject, room.transform.parent.gameObject, GetRoomType(room), room.transform.position);
            Object.Destroy(room.gameObject);
        }

        public bool IsRoomAlive(RoomComponent room)
        {
            return room?.IsAlive ?? false;
        }

        public float GetRoomHealth(RoomComponent room)
        {
            return room?.CurrentHealth ?? 0f;
        }

        public void DamageRoom(RoomComponent room, float damage)
        {
            room?.TakeDamage(damage);
        }

        public RoomType GetRoomType(RoomComponent room)
        {
            return room?.RoomType ?? RoomType.EmptyRoom;
        }

        public ShipComponent GetRoomShip(RoomComponent room)
        {
            if (room?.transform.parent == null) return null;
            return room.transform.parent.GetComponent<ShipComponent>();
        }

        public Vector3 GetRoomPosition(RoomComponent room)
        {
            return room?.transform.position ?? Vector3.zero;
        }

        public void SetRoomPosition(RoomComponent room, Vector3 position)
        {
            if (room != null)
            {
                room.transform.position = position;
            }
        }
    }
}
