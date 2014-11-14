﻿using Sensus.Exceptions;
using System;
using System.Collections.Generic;

namespace Sensus.Probes
{
    /// <summary>
    /// Initializes protocols and their probes with platform-generic bindings.
    /// </summary>
    public class ProbeInitializer
    {
        public void Initialize(List<Probe> probes)
        {
            foreach (Probe probe in probes)
                if (probe.Enabled)
                {
                    try { Initialize(probe); }
                    catch (Exception ex)
                    {
                        if (Logger.Level >= LoggingLevel.Normal)
                            Logger.Log("Probe \"" + probe.Name + "\" failed to initialize:  " + ex.Message);

                        probe.ChangeState(ProbeState.Initializing, ProbeState.InitializeFailed);
                    }
                }
        }

        protected virtual ProbeState Initialize(Probe probe)
        {
            probe.Initialize();

            return probe.State;
        }
    }
}
