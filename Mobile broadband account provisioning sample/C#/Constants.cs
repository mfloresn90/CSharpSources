﻿//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using System.Collections.Generic;
using System;

namespace SDKTemplate
{
    public partial class MainPage : SDKTemplate.Common.LayoutAwarePage
    {
        // This is used on the main page as the title of the sample.
        public const string FEATURE_NAME = "Provisioning Agent";

        // This will be used to populate the list of scenarios on the main page with
        // which the user will choose the specific scenario that they are interested in.
        List<Scenario> scenarios = new List<Scenario>
        {
            new Scenario() { Title = "Provision for mobile network operators", ClassType = typeof(ProvisioningAgent.ProvisionMno) },
            new Scenario() { Title = "Update profile cost", ClassType = typeof(ProvisioningAgent.UpdateCost) },
            new Scenario() { Title = "Update profile data usage", ClassType = typeof(ProvisioningAgent.UpdateUsage) },
            new Scenario() { Title = "Provision for other operators", ClassType = typeof(ProvisioningAgent.ProvisionOtherOperator) }
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
