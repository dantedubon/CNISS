﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest
{
    public class SupervisorRequest:IValidPost
    {
        public Guid guid { get; set; }
        public UserRequest.UserRequest userRequest { get; set; }
        public AuditoriaRequest.AuditoriaRequest auditoriaRequest { get; set; }
        public IList<LugarVisitaRequest> lugarVisitaRequests { get; set; }


        public bool isValidPost()
        {
            return userRequest != null && userRequest.isValidPost()
                &&lugarVisitaRequests!=null&&lugarVisitaRequests.All(isValidLugarVisitaForPost)
                &&auditoriaRequest!=null&&  auditoriaRequest.isValidPost();

        }

        private bool isValidLugarVisitaForPost(LugarVisitaRequest lugarVisita)
        {
            return lugarVisita.isValidPost();
        }

        public bool isValidPostFichaSupervision()
        {
            return userRequest != null&&userRequest.isValidForAuthenticate() && Guid.Empty!=guid;
        }
    }
}