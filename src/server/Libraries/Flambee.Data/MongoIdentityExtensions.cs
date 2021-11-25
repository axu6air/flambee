using Flambee.Core.Domain.Authentication;
using Flambee.Core.Domain.UserDetails;
using Flambee.Data.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flambee.Data
{
    public static class MongoIdentityExtensions
    {
        public static IdentityBuilder AddIdentityMongoDbProvider<TUser>(this IServiceCollection services)
            where TUser : User
        {
            return AddIdentityMongoDbProvider<TUser, UserRole<ObjectId>, ObjectId>(services, x => { });
        }

        public static IdentityBuilder AddIdentityMongoDbProvider<TUser, TKey>(this IServiceCollection services)
            where TKey : IEquatable<TKey>
            where TUser : User<TKey>
        {
            return AddIdentityMongoDbProvider<TUser, UserRole<TKey>, TKey>(services, x => { });
        }

        public static IdentityBuilder AddIdentityMongoDbProvider<TUser>(this IServiceCollection services,
            Action<MongoIdentityOptions> setupDatabaseAction)
            where TUser : User
        {
            return AddIdentityMongoDbProvider<TUser, UserRole, ObjectId>(services, setupDatabaseAction);
        }

        public static IdentityBuilder AddIdentityMongoDbProvider<TUser, TKey>(this IServiceCollection services,
            Action<MongoIdentityOptions> setupDatabaseAction)
            where TKey : IEquatable<TKey>
            where TUser : User<TKey>
        {
            return AddIdentityMongoDbProvider<TUser, UserRole<TKey>, TKey>(services, setupDatabaseAction);
        }

        public static IdentityBuilder AddIdentityMongoDbProvider<TUser, TRole>(this IServiceCollection services,
            Action<IdentityOptions> setupIdentityAction, Action<MongoIdentityOptions> setupDatabaseAction)
            where TUser : User
            where TRole : UserRole
        {
            return AddIdentityMongoDbProvider<TUser, TRole, ObjectId>(services, setupIdentityAction, setupDatabaseAction);
        }

        public static IdentityBuilder AddIdentityMongoDbProvider<TUser, TRole, TKey>(this IServiceCollection services,
            Action<MongoIdentityOptions> setupDatabaseAction)
            where TKey : IEquatable<TKey>
            where TUser : User<TKey>
            where TRole : UserRole<TKey>
        {
            return AddIdentityMongoDbProvider<TUser, TRole, TKey>(services, x => { }, setupDatabaseAction);
        }

        public static IdentityBuilder AddIdentityMongoDbProvider(this IServiceCollection services,
            Action<IdentityOptions> setupIdentityAction, Action<MongoIdentityOptions> setupDatabaseAction)
        {
            return AddIdentityMongoDbProvider<User, UserRole, ObjectId>(services, setupIdentityAction, setupDatabaseAction);
        }

        public static IdentityBuilder AddIdentityMongoDbProvider<TUser>(this IServiceCollection services,
            Action<IdentityOptions> setupIdentityAction, Action<MongoIdentityOptions> setupDatabaseAction) where TUser : User
        {
            return AddIdentityMongoDbProvider<TUser, UserRole, ObjectId>(services, setupIdentityAction, setupDatabaseAction);
        }

        public static IdentityBuilder AddIdentityMongoDbProvider<TUser, TRole, TKey>(this IServiceCollection services,
            Action<IdentityOptions> setupIdentityAction, Action<MongoIdentityOptions> setupDatabaseAction, IdentityErrorDescriber identityErrorDescriber = null)
            where TKey : IEquatable<TKey>
            where TUser : User<TKey>
            where TRole : UserRole<TKey>
        {
            var dbOptions = new MongoIdentityOptions();
            setupDatabaseAction(dbOptions);

            var migrationCollection = MongoUtil.FromConnectionString<MigrationHistory>(dbOptions, dbOptions.MigrationCollection);
            var migrationUserCollection = MongoUtil.FromConnectionString<MigrationMongoUser<TKey>>(dbOptions, dbOptions.UsersCollection);
            var userCollection = MongoUtil.FromConnectionString<TUser>(dbOptions, dbOptions.UsersCollection);
            var roleCollection = MongoUtil.FromConnectionString<TRole>(dbOptions, dbOptions.RolesCollection);

            // apply migrations before identity services resolved
            Migrator.Apply<MigrationMongoUser<TKey>, TRole, TKey>(migrationCollection, migrationUserCollection, roleCollection);

            var builder = services.AddIdentity<TUser, TRole>(setupIdentityAction ?? (x => { }));

            builder.AddRoleStore<RoleStore<TRole, TKey>>()
            .AddUserStore<UserStore<TUser, TRole, TKey>>()
            .AddUserManager<UserManager<TUser>>()
            .AddRoleManager<RoleManager<TRole>>()
            .AddDefaultTokenProviders();

            services.AddSingleton(x => userCollection);
            services.AddSingleton(x => roleCollection);

            // register custom ObjectId TypeConverter
            if (typeof(TKey) == typeof(ObjectId))
            {
                TypeConverterResolver.RegisterTypeConverter<ObjectId, ObjectIdConverter>();
            }

            // Identity Services
            services.AddTransient<IRoleStore<TRole>>(x => new RoleStore<TRole, TKey>(roleCollection, identityErrorDescriber));
            services.AddTransient<IUserStore<TUser>>(x => new UserStore<TUser, TRole, TKey>(userCollection, roleCollection, identityErrorDescriber));

            return builder;
        }
    }
}
