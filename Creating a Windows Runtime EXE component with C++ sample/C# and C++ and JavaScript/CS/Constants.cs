﻿//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
//
//*********************************************************

using System.Collections.Generic;
using System;

namespace SDKTemplate
{
    public partial class MainPage : SDKTemplate.Common.LayoutAwarePage
    {
        public const string FEATURE_NAME = "WRL Out-of-process Windows Runtime component";

        List<Scenario> scenarios = new List<Scenario>
        {
            new Scenario() { Title = "Using Custom Components", ClassType = typeof(WRLOutOfProcessWinRTComponent.OvenClient) },
            new Scenario() { Title = "Handling Windows Runtime Exceptions", ClassType = typeof(WRLOutOfProcessWinRTComponent.CustomException) }
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
