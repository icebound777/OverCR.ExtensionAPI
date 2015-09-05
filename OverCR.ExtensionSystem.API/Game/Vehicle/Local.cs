using Events.Car;
using Events.Player;
using UnityEngine;

namespace OverCR.ExtensionSystem.API.Game.Vehicle
{
    public static class Local
    {
        public delegate void CarDestroyedEventHandler(string cause);
        public delegate void CarRespawnedEventHandler(float px, float py, float pz, float rx, float ry, float rz, bool fastRespawn);
        public delegate void CarBrokeObjectEventHandler(int objectIndex);

        public static event CarDestroyedEventHandler CarDestroyed;
        public static event CarRespawnedEventHandler CarRespawned;
        public static event CarBrokeObjectEventHandler CarBrokeObject;

        public static float MilesPerHour
        {
            get
            {
                var localCar = GameObject.Find("LocalCar");
                var carStats = localCar?.GetComponent<CarStats>();

                return carStats?.GetMilesPerHour() ?? 0;
            }
        }

        public static float MilesPerHourB
        {
            get
            {
                var localCar = GameObject.Find("LocalCar");
                var carStats = localCar?.GetComponent<CarStats>();

                return carStats?.GetMilesPerHourB() ?? 0;
            }
        }

        public static float SmoothSpeed
        {
            get
            {
                var localCar = GameObject.Find("LocalCar");
                var carStats = localCar?.GetComponent<CarStats>();

                return carStats?.SmoothSpeed_ ?? 0;
            }
        }

        public static float SmoothVelocity
        {
            get
            {
                var localCar = GameObject.Find("LocalCar");
                var carStats = localCar?.GetComponent<CarStats>();

                return carStats?.SmoothSpeed_ ?? 0;
            }
        }

        static Local()
        {
            BrokeObject.SubscribeAll(Car_BrokeObject);
            CarRespawn.SubscribeAll(Car_Respawn);
            Death.SubscribeAll(Car_Death);
        }

        private static void Car_BrokeObject(GameObject sender, BrokeObject.Data data)
        {
            CarBrokeObject?.Invoke(data.breakableObjectIndex_);
        }

        private static void Car_Respawn(GameObject sender, CarRespawn.Data data)
        {
            CarRespawned?.Invoke(
                data.position_.x,
                data.position_.y,
                data.position_.z,
                data.rotation_.x,
                data.rotation_.y,
                data.rotation_.z,
                data.fastRespawn_
            );
        }

        private static void Car_Death(GameObject sender, Death.Data data)
        {
            switch (data.causeOfDeath)
            {
                case Death.Cause.Impact:
                    CarDestroyed?.Invoke("impact");
                    break;
                case Death.Cause.KillGrid:
                    CarDestroyed?.Invoke("kill_grid");
                    break;
                case Death.Cause.LaserOverheated:
                    CarDestroyed?.Invoke("laser_overheat");
                    break;
                case Death.Cause.Overheated:
                    CarDestroyed?.Invoke("overheat");
                    break;
                case Death.Cause.SelfTermination:
                    CarDestroyed?.Invoke("self_termination");
                    break;
                case Death.Cause.None:
                    CarDestroyed?.Invoke("none");
                    break;
                default:
                    CarDestroyed?.Invoke("unknown");
                    break;
            }
        }
    }
}
