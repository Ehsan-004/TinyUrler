from django.db import models
from django.contrib.auth.models import User
# from user_profile.models import Profile


class Link(models.Model):
    url = models.CharField(max_length=400)
    short_url = models.CharField(blank=False, null=False, max_length=150)
    # user = models.ForeignKey(Profile, on_delete=models.CASCADE)

    class Meta:
        db_table = 'links'
        verbose_name = 'Link'
        verbose_name_plural = 'Links'

    def __str__(self):
        return str(self.url)
