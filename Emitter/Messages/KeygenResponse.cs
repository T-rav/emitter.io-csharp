/*
Copyright (c) 2016 Roman Atachiants

All rights reserved. This program and the accompanying materials
are made available under the terms of the Eclipse Public License v1.0
and Eclipse Distribution License v1.0 which accompany this distribution. 

The Eclipse Public License:  http://www.eclipse.org/legal/epl-v10.html
The Eclipse Distribution License: http://www.eclipse.org/org/documents/edl-v10.php.

Contributors:
   Roman Atachiants - integrating with emitter.io
*/


using System;
using System.Collections;
using System.Text;

namespace Emitter.Network.Messages
{
    /// <summary>
    /// Represents a key generation response.
    /// </summary>
    public class KeygenResponse
    {
        /// <summary>
        /// Gets or sets the generated key.
        /// </summary>
        public string Key;

        /// <summary>
        /// Gets or sets the target channel for the supplied key.
        /// </summary>
        public string Channel;

        /// <summary>
        /// Gets or sets the status code returned by emitter.io service.
        /// </summary>
        public int Status;

        /// <summary>
        /// Deserializes the JSON key-gen response.
        /// </summary>
        /// <param name="json">The json string to deserialize from.</param>
        /// <returns></returns>
        public static KeygenResponse FromJson(string json)
        {
            var map = JsonSerializer.DeserializeString(json) as Hashtable;
            var response = new KeygenResponse();

            response.Key = (string)map["key"];
            response.Channel = (string)map["channel"];
            response.Status = (int)map["status"];
            return response;
        }

    }
}