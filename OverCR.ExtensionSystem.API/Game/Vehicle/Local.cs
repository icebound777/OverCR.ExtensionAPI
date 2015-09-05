using Events.Car;
using Events.Player;
using UnityEngine;

namespace OverCR.ExtensionSystem.API.Game.Vehicle
{
    public static class Local
    {
        public delegate void CarDestroyedEventHandler(string cause);
        public delegate void CarRespawnedEventHandler(float px, float py, float pz, float rx, float ry, float rz, bool fastRespawn);

        public static event CarDestroyedEventHandler CarDestroyed;
        public static event CarRespawnedEventHandler CarRespawned;

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
            Death.SubscribeAll(Car_Death);
            CarRespawn.SubscribeAll(Car_Respawn);
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
    }
}
