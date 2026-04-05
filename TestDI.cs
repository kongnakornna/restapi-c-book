using Amazon.Runtime.Internal.Transform;
using Esprima.Ast;
using Jtech.Common.DataStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using Novell.Directory.Ldap.Rfc2251;
using SharpCompress.Writers;
using System.Diagnostics;
using ZstdSharp.Unsafe;
using TestCommon.Models;
using Jtech.Common.Helpers;

namespace TestCommon
{


    public class TestInject
    { 
        public string message { get; set; }
    }

    public class RunningLogic
    {
        Store<RunningLogic> _store;
        TestInject _inj;
        public RunningLogic(Store<RunningLogic> store,TestInject inj)
        {
            this._store = store;
            this._inj = inj;
        }

        public string GetInjMessage()
        {
            return this._inj.message;
        }
        public Blog GetBlog(string id)
        {
            return this._store.GetById<Blog>(id).Result;
        }
    }


}
