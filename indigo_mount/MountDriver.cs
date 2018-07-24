//
// ASCOM Mount Driver for INDIGO
//
// Copyright (c) 2018 CloudMakers, s. r. o.
// All rights reserved.
//
// You can use this software under the terms of 'INDIGO Astronomy
// open-source license' (see LICENSE.md).
//
// THIS SOFTWARE IS PROVIDED BY THE AUTHORS 'AS IS' AND ANY EXPRESS
// OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
// ARE DISCLAIMED. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE
// GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
// WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
// NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#define Mount

using ASCOM.DeviceInterface;
using ASCOM.Utilities;
using INDIGO;
using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;

namespace ASCOM.INDIGO {
  [Guid("520a4be5-9287-41e1-833f-d6cce75d9647")]
  [ClassInterface(ClassInterfaceType.None)]
  public class Mount : BaseDriver, ITelescopeV3 {
    internal static string driverID = "ASCOM.INDIGO.Mount";
    private static readonly string driverName = "INDIGO Mount";

    public Mount() {
      deviceInterface = Device.InterfaceMask.Mount;
    }

    public string Description {
      get {
        return driverName + " (" + deviceName + ")";
      }
    }

    public string DriverInfo {
      get {
        return Name + " driver, version " + DriverVersion;
      }
    }

    public string DriverVersion {
      get {
        Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        return String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
      }
    }

    public short InterfaceVersion {
      get {
        return 3;
      }
    }

    public string Name {
      get {
        return driverName;
      }
    }

    private EquatorialCoordinateType equatorialSystem = EquatorialCoordinateType.equLocalTopocentric;
    private bool hasAltAz = false, canSetAltAz = false, canSetRADec = false, canSync = false, canPark = false, canSetTracking = false, canMove = false;

    override protected void PropertyAdded(Property property) {
      base.PropertyAdded(property);
      if (property.DeviceName == deviceName) {
        if (property.Name == "MOUNT_EQUATORIAL_COORDINATES") {
          canSetRADec = property.Permission == Property.Permissions.ReadWrite;
        } else if (property.Name == "MOUNT_HORIZONTAL_COORDINATES") {
          hasAltAz = true;
          canSetAltAz = property.Permission == Property.Permissions.ReadWrite;
        } else if (property.Name == "MOUNT_ON_COORDINATES_SET") {
          canSync = property.GetItem("SYNC") != null;
        } else if (property.Name == "MOUNT_PARK") {
          canPark = true;
        } else if (property.Name == "MOUNT_TRACKING") {
          canSetTracking = true;
        } else if (property.Name == "MOUNT_MOTION_RA" || property.Name == "MOUNT_MOTION_DEC") {
          canMove = true;
        }
      }
    }

    override protected void PropertyChanged(Property property) {
      base.PropertyChanged(property);
      if (property.DeviceName == deviceName) {
      }
    }

    public void AbortSlew() {
      if (IsConnected) {
        if (AtPark)
          throw new ASCOM.InvalidOperationException("AbortSlew - Parked");
        SwitchProperty MountAbortMotionProperty = (SwitchProperty)device.GetProperty("MOUNT_ABORT_MOTION");
        if (MountAbortMotionProperty != null) {
          MountAbortMotionProperty.SetSingleValue("ABORT_MOTION", true);
          return;
        }
        throw new ASCOM.InvalidOperationException("AbortSlew - missing MOUNT_ABORT_MOTION property");
      }
      throw new ASCOM.InvalidOperationException("AbortSlew - not connected");
    }

    public AlignmentModes AlignmentMode {
      get {
        throw new ASCOM.PropertyNotImplementedException("AlignmentMode", false);
      }
    }

    public double Altitude {
      get {
        if (hasAltAz) {
          if (IsConnected) {
            NumberProperty AltAzCoordProperty = (NumberProperty)device.GetProperty("MOUNT_HORIZONTAL_COORDINATES");
            if (AltAzCoordProperty != null) {
              return ((NumberItem)AltAzCoordProperty.GetItem("ALT")).Value;
            }
            throw new ASCOM.InvalidOperationException("Altitude - missing MOUNT_HORIZONTAL_COORDINATES property");
          }
          throw new ASCOM.InvalidOperationException("Altitude - not connected");
        }
        throw new ASCOM.PropertyNotImplementedException("Altitude", false);
      }
    }

    public double ApertureArea {
      get {
        throw new ASCOM.PropertyNotImplementedException("ApertureArea", false);
      }
    }

    public double ApertureDiameter {
      get {
        throw new ASCOM.PropertyNotImplementedException("ApertureDiameter", false);
      }
    }

    public bool AtHome {
      get {
        return false;
      }
    }

    public bool AtPark {
      get {
        if (IsConnected) {
          SwitchProperty MountParkProperty = (SwitchProperty)device.GetProperty("MOUNT_PARK");
          if (MountParkProperty != null) {
            return ((SwitchItem)MountParkProperty.GetItem("PARKED")).Value;
          }
          throw new ASCOM.InvalidOperationException("AtPark - missing MOUNT_PARK property");
        }
        throw new ASCOM.InvalidOperationException("AtPark - not connected");
      }
    }

    public IAxisRates AxisRates(TelescopeAxes Axis) {
      return new AxisRates(Axis);
    }

    public double Azimuth {
      get {
        if (hasAltAz) {
          if (IsConnected) {
            NumberProperty AltAzCoordProperty = (NumberProperty)device.GetProperty("MOUNT_HORIZONTAL_COORDINATES");
            if (AltAzCoordProperty != null) {
              return ((NumberItem)AltAzCoordProperty.GetItem("AZ")).Value;
            }
            throw new ASCOM.InvalidOperationException("Azimuth - missing MOUNT_HORIZONTAL_COORDINATES property");
          }
          throw new ASCOM.InvalidOperationException("Azimuth - not connected");
        }
        throw new ASCOM.PropertyNotImplementedException("Azimuth", false);
      }
    }

    public bool CanFindHome {
      get {
        return false;
      }
    }

    public bool CanMoveAxis(TelescopeAxes Axis) {
      switch (Axis) {
        case TelescopeAxes.axisPrimary: return canMove;
        case TelescopeAxes.axisSecondary: return canMove;
        case TelescopeAxes.axisTertiary: return false;
        default: throw new InvalidValueException("CanMoveAxis", Axis.ToString(), "0 to 2");
      }
    }

    public bool CanPark {
      get {
        return canPark;
      }
    }

    public bool CanPulseGuide {
      get {
        return false;
      }
    }

    public bool CanSetDeclinationRate {
      get {
        return false;
      }
    }

    public bool CanSetGuideRates {
      get {
        return false;
      }
    }

    public bool CanSetPark {
      get {
        return false;
      }
    }

    public bool CanSetPierSide {
      get {
        return false;
      }
    }

    public bool CanSetRightAscensionRate {
      get {
        return false;
      }
    }

    public bool CanSetTracking {
      get {
        return canSetTracking;
      }
    }

    public bool CanSlew {
      get {
        return canSetRADec;
      }
    }

    public bool CanSlewAltAz {
      get {
        return canSetAltAz;
      }
    }

    public bool CanSlewAltAzAsync {
      get {
        return canSetAltAz;
      }
    }

    public bool CanSlewAsync {
      get {
        return canSetRADec;
      }
    }

    public bool CanSync {
      get {
        return canSetRADec & canSync;
      }
    }

    public bool CanSyncAltAz {
      get {
        return canSetAltAz & canSync;
      }
    }

    public bool CanUnpark {
      get {
        return canPark;
      }
    }

    public double Declination {
      get {
        if (IsConnected) {
          NumberProperty EqCoordProperty = (NumberProperty)device.GetProperty("MOUNT_EQUATORIAL_COORDINATES");
          if (EqCoordProperty != null) {
            return ((NumberItem)EqCoordProperty.GetItem("DEC")).Value;
          }
          throw new ASCOM.InvalidOperationException("Declination - missing MOUNT_EQUATORIAL_COORDINATES property");
        }
        throw new ASCOM.InvalidOperationException("Declination - not connected");
      }
    }

    public double DeclinationRate {
      get {
        return 0.0;
      }
      set {
        throw new ASCOM.PropertyNotImplementedException("DeclinationRate", true);
      }
    }

    public PierSide DestinationSideOfPier(double RightAscension, double Declination) {
      throw new ASCOM.PropertyNotImplementedException("DestinationSideOfPier", false);
    }

    public bool DoesRefraction {
      get {
        throw new ASCOM.PropertyNotImplementedException("DoesRefraction", false);
      }
      set {
        throw new ASCOM.PropertyNotImplementedException("DoesRefraction", true);
      }
    }

    public EquatorialCoordinateType EquatorialSystem {
      get {
        return equatorialSystem;
      }
    }

    public void FindHome() {
      throw new ASCOM.MethodNotImplementedException("FindHome");
    }

    public double FocalLength {
      get {
        throw new ASCOM.PropertyNotImplementedException("FocalLength", false);
      }
    }

    public double GuideRateDeclination {
      get {
        throw new ASCOM.PropertyNotImplementedException("GuideRateDeclination", false);
      }
      set {
        throw new ASCOM.PropertyNotImplementedException("GuideRateDeclination", true);
      }
    }

    public double GuideRateRightAscension {
      get {
        throw new ASCOM.PropertyNotImplementedException("GuideRateRightAscension", false);
      }
      set {
        throw new ASCOM.PropertyNotImplementedException("GuideRateRightAscension", true);
      }
    }

    public bool IsPulseGuiding {
      get {
        throw new ASCOM.PropertyNotImplementedException("IsPulseGuiding", false);
      }
    }

    public void MoveAxis(TelescopeAxes Axis, double Rate) {
      if (IsConnected) {
        if (AtPark)
          throw new ASCOM.InvalidOperationException("MoveAxis - Parked");
        if (Axis != TelescopeAxes.axisPrimary && Axis != TelescopeAxes.axisSecondary)
          throw new InvalidValueException("CanMoveAxis - Axis", Axis.ToString(), "0 to 2");
        if (!(Rate == 0 || (Rate >= 1 && Rate <= 4) || (Rate >= -4 && Rate <= -1)))
          throw new InvalidValueException("CanMoveAxis - Rate", Rate.ToString(), "0, 1 to 4");
        SwitchProperty SlewRateProperty = (SwitchProperty)device.GetProperty("MOUNT_SLEW_RATE");
        if (SlewRateProperty != null) {
          switch ((int)Rate) {
            case 1:
              SlewRateProperty.SetSingleValue("GUIDE", true);
              break;
            case 2:
              SlewRateProperty.SetSingleValue("CENTERING", true);
              break;
            case 3:
              SlewRateProperty.SetSingleValue("FIND", true);
              break;
            case 4:
              SlewRateProperty.SetSingleValue("MAX", true);
              break;
          }
          if (Axis == TelescopeAxes.axisPrimary) {
            SwitchProperty MotionNSProperty = (SwitchProperty)device.GetProperty("MOUNT_MOTION_DEC");
            if (MotionNSProperty != null) {
              if (Rate > 0) {
                MotionNSProperty.SetSingleValue("NORTH", true);
              } else if (Rate < 0) {
                MotionNSProperty.SetSingleValue("SOUTH", true);
              } else {
                MotionNSProperty.SetSingleValue("NORTH", false);
                MotionNSProperty.SetSingleValue("SOUTH", false);
              }
              return;
            }
            throw new ASCOM.InvalidOperationException("MoveAxis - missing MOUNT_MOTION_DEC property");
          } else {
            SwitchProperty MotionNSProperty = (SwitchProperty)device.GetProperty("MOUNT_MOTION_RA");
            if (MotionNSProperty != null) {
              if (Rate > 0) {
                MotionNSProperty.SetSingleValue("WEST", true);
              } else if (Rate < 0) {
                MotionNSProperty.SetSingleValue("EAST", true);
              } else {
                MotionNSProperty.SetSingleValue("WEST", false);
                MotionNSProperty.SetSingleValue("EAST", false);
              }
              return;
            }
            throw new ASCOM.InvalidOperationException("MoveAxis - missing MOUNT_MOTION_RA property");
          }
        }
        throw new ASCOM.InvalidOperationException("MoveAxis - missing MOUNT_SLEW_RATE property");
      }
      throw new ASCOM.InvalidOperationException("MoveAxis - not connected");
    }

    public void Park() {
      if (IsConnected) {
        SwitchProperty MountParkProperty = (SwitchProperty)device.GetProperty("MOUNT_PARK");
        if (MountParkProperty != null) {
          MountParkProperty.SetSingleValue("PARKED", true);
          do
            Thread.Sleep(100);
          while (MountParkProperty.State == Property.States.Busy);
          return;
        }
        throw new ASCOM.InvalidOperationException("Park - missing MOUNT_PARK property");
      }
      throw new ASCOM.InvalidOperationException("Park - not connected");
    }

    public void PulseGuide(GuideDirections Direction, int Duration) {
      throw new ASCOM.MethodNotImplementedException("PulseGuide");
    }

    public double RightAscension {
      get {
        if (IsConnected) {
          NumberProperty EqCoordProperty = (NumberProperty)device.GetProperty("MOUNT_EQUATORIAL_COORDINATES");
          if (EqCoordProperty != null) {
            return ((NumberItem)EqCoordProperty.GetItem("RA")).Value;
          }
          throw new ASCOM.InvalidOperationException("RightAscension - missing MOUNT_EQUATORIAL_COORDINATES property");
        }
        throw new ASCOM.InvalidOperationException("RightAscension - not connected");
      }
    }

    public double RightAscensionRate {
      get {
        return 0.0;
      }
      set {
        throw new ASCOM.PropertyNotImplementedException("RightAscensionRate", true);
      }
    }

    public void SetPark() {
      throw new ASCOM.MethodNotImplementedException("SetPark");
    }

    public PierSide SideOfPier {
      get {
        throw new ASCOM.PropertyNotImplementedException("SideOfPier", false);
      }
      set {
        throw new ASCOM.PropertyNotImplementedException("SideOfPier", true);
      }
    }

    public double SiderealTime {
      get {
        // get greenwich sidereal time: https://en.wikipedia.org/wiki/Sidereal_time
        //double siderealTime = (18.697374558 + 24.065709824419081 * (utilities.DateUTCToJulian(DateTime.UtcNow) - 2451545.0));

        // alternative using NOVAS 3.1
        double siderealTime = 0.0;
        using (var novas = new ASCOM.Astrometry.NOVAS.NOVAS31()) {
          var jd = utilities.DateUTCToJulian(DateTime.UtcNow);
          novas.SiderealTime(jd, 0, novas.DeltaT(jd),
              ASCOM.Astrometry.GstType.GreenwichApparentSiderealTime,
              ASCOM.Astrometry.Method.EquinoxBased,
              ASCOM.Astrometry.Accuracy.Reduced, ref siderealTime);
        }
        // allow for the longitude
        siderealTime += SiteLongitude / 360.0 * 24.0;
        // reduce to the range 0 to 24 hours
        siderealTime = siderealTime % 24.0;
        return siderealTime;
      }
    }

    public double SiteElevation {
      get {
        if (IsConnected) {
          NumberProperty GeographicCoordProperty = (NumberProperty)device.GetProperty("GEOGRAPHIC_COORDINATES");
          if (GeographicCoordProperty != null) {
            return ((NumberItem)GeographicCoordProperty.GetItem("ELEVATION")).Value;
          }
          throw new ASCOM.InvalidOperationException("SiteElevation - missing GEOGRAPHIC_COORDINATES property");
        }
        throw new ASCOM.InvalidOperationException("SiteElevation - not connected");
      }
      set {
        if ((value < -300) | (value > 10000)) {
          throw new InvalidValueException("SiteLatitude", value.ToString(), "-90 to 90");
        }
        if (IsConnected) {
          NumberProperty GeographicCoordProperty = (NumberProperty)device.GetProperty("GEOGRAPHIC_COORDINATES");
          if (GeographicCoordProperty != null) {
            GeographicCoordProperty.SetSingleValue("ELEVATION", value);
            while (GeographicCoordProperty.State == Property.States.Busy)
              Thread.Sleep(100);
            return;
          }
          throw new ASCOM.InvalidOperationException("SiteElevation - missing GEOGRAPHIC_COORDINATES property");
        }
        throw new ASCOM.InvalidOperationException("SiteElevation - not connected");
      }
    }

    public double SiteLatitude {
      get {
        if (IsConnected) {
          NumberProperty GeographicCoordProperty = (NumberProperty)device.GetProperty("GEOGRAPHIC_COORDINATES");
          if (GeographicCoordProperty != null) {
            return ((NumberItem)GeographicCoordProperty.GetItem("LATITUDE")).Value;
          }
          throw new ASCOM.InvalidOperationException("SiteLatitude - missing GEOGRAPHIC_COORDINATES property");
        }
        throw new ASCOM.InvalidOperationException("SiteLatitude - not connected");
      }
      set {
        if ((value < -90) | (value > 90)) {
          throw new InvalidValueException("SiteLatitude", value.ToString(), "-90 to 90");
        }
        if (IsConnected) {
          NumberProperty GeographicCoordProperty = (NumberProperty)device.GetProperty("GEOGRAPHIC_COORDINATES");
          if (GeographicCoordProperty != null) {
            GeographicCoordProperty.SetSingleValue("LATITUDE", value);
            do
              Thread.Sleep(100);
            while (GeographicCoordProperty.State == Property.States.Busy);
            return;
          }
          throw new ASCOM.InvalidOperationException("SiteLatitude - missing GEOGRAPHIC_COORDINATES property");
        }
        throw new ASCOM.InvalidOperationException("SiteLatitude - not connected");
      }
    }

    public double SiteLongitude {
      get {
        if (IsConnected) {
          NumberProperty GeographicCoordProperty = (NumberProperty)device.GetProperty("GEOGRAPHIC_COORDINATES");
          if (GeographicCoordProperty != null) {
            return ((NumberItem)GeographicCoordProperty.GetItem("LONGITUDE")).Value - 180;
          }
          throw new ASCOM.InvalidOperationException("SiteLongitude - missing GEOGRAPHIC_COORDINATES property");
        }
        throw new ASCOM.InvalidOperationException("SiteLongitude - not connected");
      }
      set {
        if ((value < -180) | (value > 180)) {
          throw new InvalidValueException("SiteLongitude", value.ToString(), "-180 to 180");
        }
        if (IsConnected) {
          NumberProperty GeographicCoordProperty = (NumberProperty)device.GetProperty("GEOGRAPHIC_COORDINATES");
          if (GeographicCoordProperty != null) {
            GeographicCoordProperty.SetSingleValue("LONGITUDE", value + 180);
            do
              Thread.Sleep(100);
            while (GeographicCoordProperty.State == Property.States.Busy);
            return;
          }
          throw new ASCOM.InvalidOperationException("SiteLongitude - missing GEOGRAPHIC_COORDINATES property");
        }
        throw new ASCOM.InvalidOperationException("SiteLongitude - not connected");
      }
    }

    public short SlewSettleTime {
      get {
        throw new ASCOM.PropertyNotImplementedException("SlewSettleTime", false);
      }
      set {
        throw new ASCOM.PropertyNotImplementedException("SlewSettleTime", true);
      }
    }

    public void SlewToAltAz(double Azimuth, double Altitude) {
      throw new ASCOM.MethodNotImplementedException("SlewToAltAz");
    }

    public void SlewToAltAzAsync(double Azimuth, double Altitude) {
      throw new ASCOM.MethodNotImplementedException("SlewToAltAzAsync");
    }

    public void SlewToCoordinates(double RightAscension, double Declination) {
      SlewToCoordinatesAsync(RightAscension, Declination);
      do {
        Thread.Sleep(100);
      } while (Slewing);
    }

    public void SlewToCoordinatesAsync(double RightAscension, double Declination) {
      if (AtPark)
        throw new ASCOM.InvalidOperationException("SlewToCoordinatesAsync - Parked");
      if ((RightAscension < 0) | (RightAscension > 24)) {
        throw new InvalidValueException("RightAscension", RightAscension.ToString(), "0 to 24");
      }
      if ((Declination < -90) | (Declination > 90)) {
        throw new InvalidValueException("Declination", RightAscension.ToString(), "-90 to 90");
      }
      TargetRightAscension = RightAscension;
      TargetDeclination = Declination;
      if (IsConnected) {
        SwitchProperty OnCoordSetProperty = (SwitchProperty)device.GetProperty("MOUNT_ON_COORDINATES_SET");
        if (OnCoordSetProperty != null) {
          OnCoordSetProperty.SetSingleValue("TRACK", true);
          NumberProperty MountEquatorialProperty = (NumberProperty)device.GetProperty("MOUNT_EQUATORIAL_COORDINATES");
          if (MountEquatorialProperty != null) {
            MountEquatorialProperty.SetSingleValue("RA", RightAscension);
            MountEquatorialProperty.SetSingleValue("DEC", Declination);
            return;
          }
          throw new ASCOM.InvalidOperationException("SlewToCoordinatesAsync - missing MOUNT_EQUATORIAL_COORDINATES property");
        }
        throw new ASCOM.InvalidOperationException("SlewToCoordinatesAsync - missing MOUNT_ON_COORDINATES_SET property");
      }
      throw new ASCOM.InvalidOperationException("SlewToCoordinatesAsync - not connected");
    }

    public void SlewToTarget() {
      SlewToCoordinates(TargetRightAscension, TargetDeclination);
    }

    public void SlewToTargetAsync() {
      SlewToCoordinatesAsync(TargetRightAscension, TargetDeclination);
    }

    public bool Slewing {
      get {
        bool result = false;
        NumberProperty MountEquatorialProperty = (NumberProperty)device.GetProperty("MOUNT_EQUATORIAL_COORDINATES");
        if (MountEquatorialProperty != null) {
          result = MountEquatorialProperty.State == Property.States.Busy;
        }
        if (!result) {
          SwitchProperty MotionNSProperty = (SwitchProperty)device.GetProperty("MOUNT_MOTION_DEC");
          if (MotionNSProperty != null) {
            result = ((SwitchItem)MotionNSProperty.GetItem("NORTH")).Value || ((SwitchItem)MotionNSProperty.GetItem("SOUTH")).Value;
          }
        }
        if (!result) {
          SwitchProperty MotionWEProperty = (SwitchProperty)device.GetProperty("MOUNT_MOTION_RA");
          if (MotionWEProperty != null) {
            result = ((SwitchItem)MotionWEProperty.GetItem("WEST")).Value || ((SwitchItem)MotionWEProperty.GetItem("EAST")).Value;
          }
        }
        return result;
      }
    }

    public void SyncToAltAz(double Azimuth, double Altitude) {
      throw new ASCOM.MethodNotImplementedException("SyncToAltAz");
    }

    public void SyncToCoordinates(double RightAscension, double Declination) {
      if (AtPark)
        throw new ASCOM.InvalidOperationException("SyncToCoordinates - Parked");
      if ((RightAscension < 0) | (RightAscension > 24)) {
        throw new InvalidValueException("RightAscension", RightAscension.ToString(), "0 to 24");
      }
      if ((Declination < -90) | (Declination > 90)) {
        throw new InvalidValueException("Declination", RightAscension.ToString(), "-90 to 90");
      }
      TargetRightAscension = RightAscension;
      TargetDeclination = Declination;
      if (IsConnected) {
        SwitchProperty OnCoordSetProperty = (SwitchProperty)device.GetProperty("MOUNT_ON_COORDINATES_SET");
        if (OnCoordSetProperty != null) {
          OnCoordSetProperty.SetSingleValue("SYNC", true);
          NumberProperty MountEquatorialProperty = (NumberProperty)device.GetProperty("MOUNT_EQUATORIAL_COORDINATES");
          if (MountEquatorialProperty != null) {
            MountEquatorialProperty.SetSingleValue("RA", RightAscension);
            MountEquatorialProperty.SetSingleValue("DEC", Declination);
            return;
          }
          throw new ASCOM.InvalidOperationException("SyncToCoordinates - missing MOUNT_EQUATORIAL_COORDINATES property");
        }
        throw new ASCOM.InvalidOperationException("SyncToCoordinates - missing MOUNT_ON_COORDINATES_SET property");
      }
      throw new ASCOM.InvalidOperationException("SyncToCoordinates - not connected");
    }

    public void SyncToTarget() {
      SyncToCoordinates(TargetRightAscension, TargetDeclination);
    }

    private double targetDeclination = -10000;

    public double TargetDeclination {
      get {
        if (targetDeclination == -10000)
          throw new ASCOM.InvalidOperationException("TargetDeclination - Value is not set");
        return targetDeclination;
      }
      set {
        if ((value < -90) | (value > 90)) {
          throw new InvalidValueException("TargetDeclination", value.ToString(), "-90 to 90");
        }
        targetDeclination = value;
      }
    }

    private double targetRightAscension = -10000;

    public double TargetRightAscension {
      get {
        if (targetRightAscension == -10000)
          throw new ASCOM.InvalidOperationException("TargetRightAscension - Value is not set");
        return targetRightAscension;
      }
      set {
        if ((value < 0) | (value > 24)) {
          throw new InvalidValueException("TargetRightAscension", value.ToString(), "0 to 24");
        }
        targetRightAscension = value;
      }
    }

    public bool Tracking {
      get {
        if (IsConnected) {
          SwitchProperty TrackingProperty = (SwitchProperty)device.GetProperty("MOUNT_TRACKING");
          if (TrackingProperty != null) {
            return ((SwitchItem)TrackingProperty.GetItem("ON")).Value;
          }
          throw new ASCOM.InvalidOperationException("Tracking - missing MOUNT_TRACKING property");
        }
        throw new ASCOM.InvalidOperationException("Tracking - not connected");
      }
      set {
        if (IsConnected) {
          SwitchProperty TrackingProperty = (SwitchProperty)device.GetProperty("MOUNT_TRACKING");
          if (TrackingProperty != null) {
            TrackingProperty.SetSingleValue(value ? "ON" : "OFF", true);
            do
              Thread.Sleep(100);
            while (TrackingProperty.State == Property.States.Busy);
            return;
          }
          throw new ASCOM.InvalidOperationException("Tracking - missing MOUNT_TRACKING property");
        }
        throw new ASCOM.InvalidOperationException("Tracking - not connected");
      }
    }

    public DriveRates TrackingRate {
      get {
        if (IsConnected) {
          SwitchProperty TrackRateProperty = (SwitchProperty)device.GetProperty("MOUNT_TRACK_RATE");
          if (TrackRateProperty != null) {
            if (((SwitchItem)TrackRateProperty.GetItem("SOLAR")).Value)
              return DriveRates.driveSolar;
            if (((SwitchItem)TrackRateProperty.GetItem("LUNAR")).Value)
              return DriveRates.driveLunar;
            return DriveRates.driveSidereal;
          }
          throw new ASCOM.InvalidOperationException("TrackingRate - missing MOUNT_TRACK_RATE property");
        }
        throw new ASCOM.InvalidOperationException("TrackingRate - not connected");
      }
      set {
        if (IsConnected) {
          SwitchProperty TrackRateProperty = (SwitchProperty)device.GetProperty("MOUNT_TRACK_RATE");
          if (TrackRateProperty != null) {
            switch (value) {
              case DriveRates.driveSolar:
                TrackRateProperty.SetSingleValue("SOLAR", true);
                break;
              case DriveRates.driveLunar:
                TrackRateProperty.SetSingleValue("LUNAR", true);
                break;
              default:
                TrackRateProperty.SetSingleValue("SIDEREAL", true);
                break;
            }
            do
              Thread.Sleep(100);
            while (TrackRateProperty.State == Property.States.Busy);
            return;
          }
          throw new ASCOM.InvalidOperationException("TrackingRate - missing MOUNT_TRACK_RATE property");
        }
        throw new ASCOM.InvalidOperationException("TrackingRate - not connected");
      }
    }

    public ITrackingRates TrackingRates {
      get {
        return new TrackingRates();
      }
    }

    public DateTime UTCDate {
      get {
        return DateTime.UtcNow;
      }
      set {
        throw new ASCOM.PropertyNotImplementedException("UTCDate", true);
      }
    }

    public void Unpark() {
      if (IsConnected) {
        SwitchProperty MountParkProperty = (SwitchProperty)device.GetProperty("MOUNT_PARK");
        if (MountParkProperty != null) {
          MountParkProperty.SetSingleValue("UNPARKED", true);
          do
            Thread.Sleep(100);
          while (MountParkProperty.State == Property.States.Busy);
          return;
        }
        throw new ASCOM.InvalidOperationException("Unpark - missing MOUNT_PARK property");
      }
      throw new ASCOM.InvalidOperationException("Unpark - not connected");
    }

    private static void RegUnregASCOM(bool bRegister) {
      using (var P = new ASCOM.Utilities.Profile()) {
        P.DeviceType = "Telescope";
        if (bRegister) {
          P.Register(driverID, driverName);
        } else {
          P.Unregister(driverID);
        }
      }
    }

    [ComRegisterFunction]
    public static void RegisterASCOM(Type t) {
      RegUnregASCOM(true);
    }

    [ComUnregisterFunction]
    public static void UnregisterASCOM(Type t) {
      RegUnregASCOM(false);
    }

    override protected void ReadProfile() {
      using (Profile driverProfile = new Profile()) {
        driverProfile.DeviceType = "Telescope";
        deviceName = driverProfile.GetValue(driverID, deviceNameProfileName, string.Empty, string.Empty);
      }
    }

    override protected void WriteProfile() {
      using (Profile driverProfile = new Profile()) {
        driverProfile.DeviceType = "Telescope";
        driverProfile.WriteValue(driverID, deviceNameProfileName, deviceName);
      }
    }
  }
}
