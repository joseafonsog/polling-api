using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PollingApp.Admin;
using PollingApp.Commons.Infrastructure;
using PollingApp.Commons.Settings;
using PollingApp.Core;
using PollingApp.DAL.Data;
using System;

namespace PollingApp.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<QuestionsDatabaseSettings>(
                Configuration.GetSection(nameof(QuestionsDatabaseSettings)));

            services.AddSingleton<IQuestionsDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<QuestionsDatabaseSettings>>().Value);

            services.Configure<EmailSettings>(
                Configuration.GetSection(nameof(EmailSettings)));

            services.AddSingleton<IEmailSettings>(sp =>
                sp.GetRequiredService<IOptions<EmailSettings>>().Value);

            services.AddSingleton<IDbService, DbService>();

            services.AddScoped<IShareAdmin, ShareAdmin>();
            services.AddScoped<IQuestionsAdmin, QuestionsAdmin>();
            services.AddScoped<ISmtpClient, SmtpClientWrapper>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                InitializeDatabase(app);
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var db = services.GetService<IDbService>();

                if (db.LoadRecords<QuestionModel>("Questions",1,0,null).Count == 0)
                {
                    db.InsertManyRecords("Questions",DbSeed.GetQuestions());
                } 
            }
        }
    }
}
