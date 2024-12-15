from django.http import JsonResponse, HttpResponseNotFound, HttpResponseRedirect
from django.shortcuts import render, redirect
from .models import Link
import string
import random
# from user_profile.models import Profile


def generate_short_id(length=6):
    characters = string.ascii_letters + string.digits
    return ''.join(random.choice(characters) for _ in range(length))


def index(request):
    if request.method == 'GET':
        # if request.user and request.user.is_authenticated:
        #     profile = Profile.objects.get(profile_user=request.user)
        # else: profile = None
        # context = {'profile':'profile'}
        return render(request, 'index.html', )


def url_shorter(req):
    if req.method == "POST":
        url = req.POST.get('url', False)

        if not url:
            return JsonResponse({'error': True, 'error_code': '1', 'error_text': 'url is empty'})

        for link in Link.objects.all():
            if link.url == url:
                short_url = link.short_url
                return JsonResponse({
                    'error': True,
                    'error_code': '2',
                    'error_text': 'this url already exists!',
                    'short_url': short_url
                })

        short_urls = [link.short_url for link in Link.objects.all()]
        while True:
            short_url = generate_short_id()
            if short_url not in short_urls:
                break
        try:
            link = Link.objects.create(url=url, short_url=short_url)
            link.save()
            return JsonResponse({'error': False, 'short_url': link.short_url, 'main_url': link.url})
        except Exception as e:
            return JsonResponse({'error': True, 'error_code': '3', 'error_text': str(e)})


def divert_url(req, short_url):
    if req.method == "GET":
        try:
            link = Link.objects.get(short_url=short_url)
            return HttpResponseRedirect(link.url)
        except:
            return HttpResponseNotFound("this url doesn't exist!")
