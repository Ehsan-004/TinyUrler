from django.db import models
from django.contrib.auth.models import User


# class Profile(models.Model):
#     profile_user = models.OneToOneField(User, on_delete=models.CASCADE)
#     image = models.ImageField(upload_to='profile_pics', default='profile.jpg')
#
#     def __str__(self):
#         return self.profile_user.username
#
#     class Meta:
#         db_table = 'user_profile'
#         verbose_name = 'Profile'
#         verbose_name_plural = 'Profiles'
