# Generated by Django 5.1.3 on 2024-11-18 19:47

from django.db import migrations


class Migration(migrations.Migration):

    dependencies = [
        ('link_shorter', '0002_alter_link_short_url_alter_link_url'),
    ]

    operations = [
        migrations.RemoveField(
            model_name='link',
            name='user',
        ),
    ]