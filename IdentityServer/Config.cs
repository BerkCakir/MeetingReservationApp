// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources =>
              new ApiResource[]
              {
                  new ApiResource("roomreservation"){Scopes={ "room_reservation_full_permission" } },
                  new ApiResource("inventoryreservation"){Scopes={ "inventory_reservation_full_permission" } },
                  new ApiResource("gateway"){Scopes={ "gateway_full_permission" } },
                  new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
              };

        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.Email(),
                       new IdentityResources.OpenId(),
                       new IdentityResources.Profile(),
                       new IdentityResource(){ Name="roles", DisplayName="Roles", Description="User Roles", UserClaims=new []{"role" } }
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("room_reservation_full_permission","RoomReservation API Authorization"),
                new ApiScope("inventory_reservation_full_permission","InventoryReservation API Authorization"),
                new ApiScope("gateway_full_permission","Gateway erişimi"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientName="MeetingReservation API",
                    ClientId = "WebClient",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "gateway_full_permission", IdentityServerConstants.LocalApi.ScopeName }
                },
                new Client
                {
                    ClientName="MeetingReservation API",
                    ClientId = "WebClientForUser",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { "room_reservation_full_permission","inventory_reservation_full_permission",
                                    IdentityServerConstants.StandardScopes.Email, IdentityServerConstants.StandardScopes.OpenId,
                                    IdentityServerConstants.StandardScopes.Profile, IdentityServerConstants.StandardScopes.OfflineAccess,
                                    IdentityServerConstants.LocalApi.ScopeName ,"roles"},
                    AccessTokenLifetime = 3600,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    AllowOfflineAccess = true
                },
                new Client
                {
                    ClientName="Token Exchange Client",
                    ClientId = "TokenExchangeClient",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes = new[] { "urn:ietf:params:oauth:grant-type:token-exchange" },
                    AllowedScopes = { "room_reservation_full_permission", "inventory_reservation_full_permission", IdentityServerConstants.StandardScopes.OpenId }
                }
            };
    }
}