using UnityEngine;

namespace FTL.Core.Events
{
    public enum RoomType
    {
        EmptyRoom
    }

    public enum WeaponType
    {
        LaserCannon
    }

    public enum TargetType
    {
        Room
    }

    [System.Serializable]
    public class ShipEventData
    {
        public GameObject ship;
        public string shipName;
        public Vector3 position;

        public ShipEventData(GameObject ship, string shipName, Vector3 position)
        {
            this.ship = ship;
            this.shipName = shipName;
            this.position = position;
        }
    }

    [System.Serializable]
    public class RoomDamageEventData
    {
        public GameObject room;
        public GameObject ship;
        public RoomType roomType;
        public float damageAmount;
        public float newHealth;
        public Vector3 hitPosition;

        public RoomDamageEventData(GameObject room, GameObject ship, RoomType roomType,
            float damageAmount, float newHealth, Vector3 hitPosition)
        {
            this.room = room;
            this.ship = ship;
            this.roomType = roomType;
            this.damageAmount = damageAmount;
            this.newHealth = newHealth;
            this.hitPosition = hitPosition;
        }
    }

    [System.Serializable]
    public class ShieldHitEventData
    {
        public GameObject ship;
        public int shieldLayer;
        public int remainingLayers;
        public Vector3 hitPosition;
        public bool shieldDestroyed;

        public ShieldHitEventData(GameObject ship, int shieldLayer, int remainingLayers,
            Vector3 hitPosition, bool shieldDestroyed)
        {
            this.ship = ship;
            this.shieldLayer = shieldLayer;
            this.remainingLayers = remainingLayers;
            this.hitPosition = hitPosition;
            this.shieldDestroyed = shieldDestroyed;
        }
    }

    [System.Serializable]
    public class WeaponFiredEventData
    {
        public GameObject weapon;
        public GameObject ship;
        public WeaponType weaponType;
        public Vector3 firePosition;
        public Vector3 targetPosition;
        public float damage;

        public WeaponFiredEventData(GameObject weapon, GameObject ship, WeaponType weaponType,
            Vector3 firePosition, Vector3 targetPosition, float damage)
        {
            this.weapon = weapon;
            this.ship = ship;
            this.weaponType = weaponType;
            this.firePosition = firePosition;
            this.targetPosition = targetPosition;
            this.damage = damage;
        }
    }

    [System.Serializable]
    public class TargetSelectedEventData
    {
        public GameObject target;
        public GameObject ship;
        public TargetType targetType;
        public bool isValidTarget;

        public TargetSelectedEventData(GameObject target, GameObject ship, TargetType targetType, bool isValidTarget)
        {
            this.target = target;
            this.ship = ship;
            this.targetType = targetType;
            this.isValidTarget = isValidTarget;
        }
    }
}