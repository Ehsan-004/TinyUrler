from django.contrib import admin
from .models import Link


class AdminLink(admin.ModelAdmin):
    list_display = ("url", "short_url")


admin.site.register(Link, AdminLink)
