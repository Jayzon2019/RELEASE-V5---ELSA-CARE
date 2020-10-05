﻿using InLife.Store.Api.Helpers;
using InLife.Store.Api.Models;
using InLife.Store.Api.Repos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InLife.Store.Api.Services
{
    public class FooterLinksService
    {
        FooterLinksRepo FLR = new FooterLinksRepo();
        LogsRepo lR = new LogsRepo();

        public TblFooterLinks GetFooterLinks(ref string log)
        {
            try
            {
                return FLR.GetFooterLinks(ref log);

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }
    }
    }
