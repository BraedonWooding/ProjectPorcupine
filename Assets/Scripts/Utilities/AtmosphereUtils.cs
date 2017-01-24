﻿#region License
// ====================================================
// Project Porcupine Copyright(C) 2016 Team Porcupine
// This program comes with ABSOLUTELY NO WARRANTY; This is free software, 
// and you are welcome to redistribute it under certain conditions; See 
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion
using System;
using UnityEngine;
using ProjectPorcupine.Rooms;

public static class AtmosphereUtils
{
    public static void EqualizeRooms(Room room1, Room room2, float maxGasToMove)
    {
        Room highPressureRoom, lowPressureRoom;
        if (room1.GetGasPressure() >= room2.GetGasPressure())
        {
            highPressureRoom = room1;
            lowPressureRoom = room2;
        }
        else
        {
            highPressureRoom = room2;
            lowPressureRoom = room1;
        }

        float targetPressure = (room1.Atmosphere.GetGasAmount() + room2.Atmosphere.GetGasAmount()) / (room1.TileCount + room2.TileCount);
        float gasNeededForTargetPressure = (targetPressure - highPressureRoom.GetGasPressure()) * highPressureRoom.TileCount;
        float gasMoved = Mathf.Min(maxGasToMove, gasNeededForTargetPressure);

        highPressureRoom.Atmosphere.MoveGasTo(lowPressureRoom.Atmosphere, gasMoved);
    }

    public static void SplitAtmosphere(AtmosphereComponent source, AtmosphereComponent destination, float ratio)
    {
        source.MoveGasTo(destination, ratio * source.GetGasAmount());
    }
}
    