using UnityEngine;
using Sim_Racing_UDP_Receiver.Games.F1_2022;
using SimRacing.Telemetry.Receiver.F1_23.Packets;

public class F1Connector : MonoBehaviour
{
    F1_23_Telemetry_Receiver receiver = new F1_23_Telemetry_Receiver("127.0.0.1", 20777);

    void Start()
    {
        receiver.MotionDataPacketReceived += (sender, e) => {
            PacketMotionData carMotionData = e.Packet;
            int playerIndex = carMotionData.playerCarIndex;
            UIManager.Singleton.SetLongG(carMotionData.carMotionData[playerIndex].gForceLongitudinal);
            UIManager.Singleton.SetLatG(carMotionData.carMotionData[playerIndex].gForceLateral);
            MotorController.Singleton.SetG(carMotionData.carMotionData[playerIndex].gForceLongitudinal, carMotionData.carMotionData[playerIndex].gForceLateral);
        };

        receiver.CarTelemetryDataPacketReceived += (sender, e) => {
            PacketCarTelemetryData carTelemetryData = e.Packet;
            int playerIndex = carTelemetryData.playerCarIndex;
            UIManager.Singleton.SetSpeed(carTelemetryData.carTelemetryData[playerIndex].speed);
        };

        receiver.StartReceiving();
    }

}
