﻿using System;
using System.Collections.Generic;
using TestJWTCoreApp.Extensions;
using TestJWTCoreApp.Models;

namespace TestJWTCoreApp.Services
{
    public class RolesProvider : IRolesService
    {
        public List<RolesViewModel> GetRolesForView()
        {
            var result = new List<RolesViewModel>();
            foreach (RolesTypes ee in Enum.GetValues(typeof(RolesTypes)))
            {
                result.Add(new RolesViewModel
                {
                    Value = (int)ee,
                    Text = EnumExtension.GetEnumDisplayName(ee)
                });
            }

            return result;
        }
    }
}
