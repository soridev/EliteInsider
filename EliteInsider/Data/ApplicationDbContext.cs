﻿using EliteInsider.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EliteInsider.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public virtual DbSet<RaidEncounter> RaidEncounters { get; set; }
        public virtual DbSet<RaidKillTime> RaidKillTimes { get; set; }
        public virtual DbSet<PlayerInfo> PlayerInfos { get; set; }
        public virtual DbSet<Guild> Guilds { get; set; }
        public virtual DbSet<GuildMember> GuildMembers { get; set; }
        public virtual DbSet<GuildLog> GuildLogs { get; set; }

    }
}