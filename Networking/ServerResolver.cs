using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Speech.Recognition;

namespace Voxel.Networking
{
    public class ServerResolver : IServerResolver
    {
        private Uri _serverLocation;

        public ServerResolver(string ServerLocation)
        {
            _serverLocation = new Uri(ServerLocation);
        }

        private HttpClient GetClient()
        {
            try
            {
                var client = new HttpClient();
                var response = client.GetAsync(_serverLocation).Result;
                bool connected = response.IsSuccessStatusCode;

                if (connected)
                    return client;
                else
                    SendServerConnectionEvent(this, new ConnectionEventArgs(ConnectionStatus.ServerBusy, response));
            }
            catch (HttpRequestException e)
            {
                SendServerConnectionEvent(this, new ConnectionEventArgs(ConnectionStatus.ServerNotFound, null, e));
            }
            return null;
        }

        public void SendVoice(RecognizedAudio audio)
        {
            using (var client = GetClient())
            using (var audioStream = new MemoryStream())
            {
                if (client == null) return;
                
                audio.WriteToAudioStream(audioStream);
                var response = client.PutAsync(_serverLocation, new StreamContent(audioStream)).Result;
                SendServerConnectionEvent(this, new ConnectionEventArgs(response.IsSuccessStatusCode ? ConnectionStatus.Success : ConnectionStatus.ServerBusy, response));
            }
        }
    }

    public class ConnectionEventArgs : EventArgs
    {
        public ServerResolver.ConnectionStatus Status;
        public HttpResponseMessage Response;
        public HttpRequestException InnerException;

        private ConnectionEventArgs() { }

        public ConnectionEventArgs(ServerResolver.ConnectionStatus Status, HttpResponseMessage Response)
        {
            this.Status = Status;
            this.Response = Response;
        }
        public ConnectionEventArgs(ServerResolver.ConnectionStatus Status, HttpResponseMessage Response, HttpRequestException InnerException) : this(Status, Response)
        {
            this.InnerException = InnerException;
        }
    }
}
