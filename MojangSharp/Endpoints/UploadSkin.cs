﻿using MojangSharp.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MojangSharp.Endpoints
{

    /// <summary>
    /// Upload Skin endpoint class
    /// </summary>
    public class UploadSkin : IEndpoint<Response>
    {

        /// <summary>
        /// Chosen skin local path
        /// </summary>
        public FileInfo Skin { get; internal set; }

        /// <summary>
        /// Creates a change skin request with a given UUID.
        /// </summary>
        /// <param name="accessToken">Access Token of the player.</param>
        /// <param name="uuid">UUID of the player.</param>
        /// <param name="skin">Path to the skin.</param>
        /// <param name="slim">Defines if slim model is used.</param>
        public UploadSkin(string accessToken, string uuid, FileInfo skin, bool slim = false)
        {
            this.Address = new Uri($"https://api.mojang.com/user/profile/{uuid}/skin");
            this.Arguments.Add(accessToken);
            this.Arguments.Add(slim.ToString());
            this.Skin = skin;
        }

        /// <summary>
        /// Performs the skin change.
        /// </summary>
        public async override Task<Response> PerformRequest()
        {
            this.Response = await Requester.Put(this, this.Skin);

            if (this.Response.Code == HttpStatusCode.NoContent || this.Response.IsSuccess)
                return new Response(this.Response) { IsSuccess = true };
            else
                return new Response(this.Response);
        }
    }
}
