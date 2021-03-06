//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
//
//*********************************************************

using System.Collections.Generic;
using System;
using CustomDeviceAccess;

namespace SDKTemplate
{
    public partial class MainPage
    {
        // Change the string below to reflect the name of your sample.
        // This is used on the main page as the title of the sample.
        public const string FEATURE_NAME = "CustomDeviceAccess";

        // Change the array below to reflect the name of your scenarios.
        // This will be used to populate the list of scenarios on the main page with
        // which the user will choose the specific scenario that they are interested in.
        // These should be in the form: "Navigating to a web page".
        // The code in MainPage will take care of turning this into: "1) Navigating to a web page"
        List<Scenario> scenarios = new List<Scenario>
        {
            new Scenario() { Title = "Connecting to the Fx2 Device",          ClassType = typeof(CustomDeviceAccess.DeviceConnect) },
            new Scenario() { Title = "Sending IOCTLs to and from the device", ClassType = typeof(CustomDeviceAccess.DeviceIO) },
            new Scenario() { Title = "Handling asynchronous device events",   ClassType = typeof(CustomDeviceAccess.DeviceEvents) },
            new Scenario() { Title = "Read and Write operations",             ClassType = typeof(CustomDeviceAccess.DeviceReadWrite) },
        };
    }

    public class Scenario
    {
        public string Title { get; set; }

        public Type ClassType { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
