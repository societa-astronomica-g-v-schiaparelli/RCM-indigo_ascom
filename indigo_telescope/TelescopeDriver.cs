//
// ASCOM Telescope Driver for INDIGO
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

#define Telescope

using ASCOM.DeviceInterface;
using ASCOM.Utilities;
using INDIGO;
using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace ASCOM.INDIGO {
  [Guid("520a4be5-9287-41e1-833f-d6cce75d9647")]
  [ClassInterface(ClassInterfaceType.None)]
  public class Telescope : BaseDriver, ITelescopeV3 {
    internal static string driverID = "ASCOM.INDIGO.Telescope";
    private static string driverName = "INDIGO Camera";

    public Telescope() {
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
        return 2;
      }
    }

    public string Name {
      get {
        return driverName;
      }
    }

    public void AbortSlew() {
      throw new ASCOM.MethodNotImplementedException("AbortSlew");
    }

    public AlignmentModes AlignmentMode {
      get {
        throw new ASCOM.PropertyNotImplementedException("AlignmentMode", false);
      }
    }

    public double Altitude {
      get {
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
        return false;
      }
    }

    public IAxisRates AxisRates(TelescopeAxes Axis) {
      return new AxisRates(Axis);
    }

    public double Azimuth {
      get {
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
        case TelescopeAxes.axisPrimary: return false;
        case TelescopeAxes.axisSecondary: return false;
        case TelescopeAxes.axisTertiary: return false;
        default: throw new InvalidValueException("CanMoveAxis", Axis.ToString(), "0 to 2");
      }
    }

    public bool CanPark {
      get {
        return false;
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
        return false;
      }
    }

    public bool CanSlew {
      get {
        return false;
      }
    }

    public bool CanSlewAltAz {
      get {
        return false;
      }
    }

    public bool CanSlewAltAzAsync {
      get {
        return false;
      }
    }

    public bool CanSlewAsync {
      get {
        return false;
      }
    }

    public bool CanSync {
      get {
        return false;
      }
    }

    public bool CanSyncAltAz {
      get {
        return false;
      }
    }

    public bool CanUnpark {
      get {
        return false;
      }
    }

    public double Declination {
      get {
        double declination = 0.0;
        return declination;
      }
    }

    public double DeclinationRate {
      get {
        double declination = 0.0;
        return declination;
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
        EquatorialCoordinateType equatorialSystem = EquatorialCoordinateType.equLocalTopocentric;
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
      throw new ASCOM.MethodNotImplementedException("MoveAxis");
    }

    public void Park() {
      throw new ASCOM.MethodNotImplementedException("Park");
    }

    public void PulseGuide(GuideDirections Direction, int Duration) {
      throw new ASCOM.MethodNotImplementedException("PulseGuide");
    }

    public double RightAscension {
      get {
        double rightAscension = 0.0;
        return rightAscension;
      }
    }

    public double RightAscensionRate {
      get {
        double rightAscensionRate = 0.0;
        return rightAscensionRate;
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
        double siderealTime = (18.697374558 + 24.065709824419081 * (utilities.DateUTCToJulian(DateTime.UtcNow) - 2451545.0));
        return siderealTime;
      }
    }

    public double SiteElevation {
      get {
        throw new ASCOM.PropertyNotImplementedException("SiteElevation", false);
      }
      set {
        throw new ASCOM.PropertyNotImplementedException("SiteElevation", true);
      }
    }

    public double SiteLatitude {
      get {
        throw new ASCOM.PropertyNotImplementedException("SiteLatitude", false);
      }
      set {
        throw new ASCOM.PropertyNotImplementedException("SiteLatitude", true);
      }
    }

    public double SiteLongitude {
      get {
        throw new ASCOM.PropertyNotImplementedException("SiteLongitude", false);
      }
      set {
        throw new ASCOM.PropertyNotImplementedException("SiteLongitude", true);
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
      throw new ASCOM.MethodNotImplementedException("SlewToCoordinates");
    }

    public void SlewToCoordinatesAsync(double RightAscension, double Declination) {
      throw new ASCOM.MethodNotImplementedException("SlewToCoordinatesAsync");
    }

    public void SlewToTarget() {
      throw new ASCOM.MethodNotImplementedException("SlewToTarget");
    }

    public void SlewToTargetAsync() {
      throw new ASCOM.MethodNotImplementedException("SlewToTargetAsync");
    }

    public bool Slewing {
      get {
        throw new ASCOM.PropertyNotImplementedException("Slewing", false);
      }
    }

    public void SyncToAltAz(double Azimuth, double Altitude) {
      throw new ASCOM.MethodNotImplementedException("SyncToAltAz");
    }

    public void SyncToCoordinates(double RightAscension, double Declination) {
      throw new ASCOM.MethodNotImplementedException("SyncToCoordinates");
    }

    public void SyncToTarget() {
      throw new ASCOM.MethodNotImplementedException("SyncToTarget");
    }

    public double TargetDeclination {
      get {
        throw new ASCOM.PropertyNotImplementedException("TargetDeclination", false);
      }
      set {
        throw new ASCOM.PropertyNotImplementedException("TargetDeclination", true);
      }
    }

    public double TargetRightAscension {
      get {
        throw new ASCOM.PropertyNotImplementedException("TargetRightAscension", false);
      }
      set {
        throw new ASCOM.PropertyNotImplementedException("TargetRightAscension", true);
      }
    }

    public bool Tracking {
      get {
        bool tracking = true;
        return tracking;
      }
      set {
        throw new ASCOM.PropertyNotImplementedException("Tracking", true);
      }
    }

    public DriveRates TrackingRate {
      get {
        throw new ASCOM.PropertyNotImplementedException("TrackingRate", false);
      }
      set {
        throw new ASCOM.PropertyNotImplementedException("TrackingRate", true);
      }
    }

    public ITrackingRates TrackingRates {
      get {
        ITrackingRates trackingRates = new TrackingRates();
        return trackingRates;
      }
    }

    public DateTime UTCDate {
      get {
        DateTime utcDate = DateTime.UtcNow;
        return utcDate;
      }
      set {
        throw new ASCOM.PropertyNotImplementedException("UTCDate", true);
      }
    }

    public void Unpark() {
      throw new ASCOM.MethodNotImplementedException("Unpark");
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
