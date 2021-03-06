﻿using Net.Qiujuer.Blink.Box;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Qiujuer.Blink.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class BlinkParser : IBlinkParser
    {
        private long mId = 0;
        protected IResource mResource;

        public BlinkParser(IResource resource)
        {
            mResource = resource;
        }

        public ReceivePacket ParseReceive(int type, int len)
        {
            long id = ++mId;
            ReceivePacket packet = null;
            switch (type)
            {
                case BlinkPacket.Type.STRING:
                    packet = new StringReceivePacket(id, type, len);
                    break;
                case BlinkPacket.Type.BYTES:
                    packet = new ByteReceivePacket(id, type, len); ;
                    break;
                case BlinkPacket.Type.FILE:
                    String file = mResource.Create(id);
                    if (file != null)
                        packet = new FileReceivePacket(id, type, len, file);
                    break;
            }
            return packet;
        }
    }
}
