from django.urls import path
from .views import index, url_shorter, divert_url

app_name = 'index'

urlpatterns = [
    path('', index, name='index'),
    path('url_shorter/', url_shorter, name='link_shorter'),
    path('<str:short_url>/', divert_url),
]
