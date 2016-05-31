namespace SmartStore.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Core.Domain;
    using Core.Domain.DataExchange;
    using Setup;

    public partial class PaidService : DbMigration, ILocaleResourcesProvider, IDataSeeder<SmartObjectContext>
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "IsPaidService", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.Product", "PaidServiceExpirationDays", c => c.Int());
            AddColumn("dbo.Product", "PaidFields", c => c.String(nullable: true, maxLength: 400));
        }

        public override void Down()
        {
            DropColumn("dbo.Product", "IsPaidService");
            DropColumn("dbo.Product", "PaidServiceExpirationDays");
            DropColumn("dbo.Product", "PaidFields");
        }

        public bool RollbackOnFailure
        {
            get { return false; }
        }

        public void Seed(SmartObjectContext context)
        {
            context.MigrateLocaleResources(MigrateLocaleResources);

        }

        public void MigrateLocaleResources(LocaleResourcesBuilder builder)
        {
            builder.AddOrUpdate("Admin.Catalog.Products.Fields.IsPaidService",
                "Is Paid Service",
                "��Ա���ѷ���",
                "Is Paid Service",
                "��Ա���ѷ���");

            builder.AddOrUpdate("Admin.Catalog.Products.Fields.PaidServiceExpirationDays",
                "Expiration Days",
                "��Ч����",
                "Expiration Days",
                "��Ч����");

            builder.AddOrUpdate("Admin.Catalog.Products.Fields.PaidFields",
                "Custom Fields",
                "�Զ����ֶ�",
                "custom fields, separated by a comma ','",
                "���ӵ��Զ����ֶΣ��Զ���','�ָ�");
        }
    }
}
