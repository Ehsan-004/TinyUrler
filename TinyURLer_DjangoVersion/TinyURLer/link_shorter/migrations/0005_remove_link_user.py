# Generated by Django 5.1.3 on 2024-11-26 18:31

from django.db import migrations


class Migration(migrations.Migration):

    dependencies = [
        ('link_shorter', '0004_link_user'),
    ]

    operations = [
        migrations.RemoveField(
            model_name='link',
            name='user',
        ),
    ]
