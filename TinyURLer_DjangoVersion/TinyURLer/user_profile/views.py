from django.shortcuts import render
from .models import Profile
from django.views import View
from django.shortcuts import redirect
from django.contrib.auth import login, logout, authenticate


def register_(req):
    if req.method == 'GET':
        pass
    if req.method == 'POST':
        pass


class LoginView(View):
    http_method_names = ['get', 'post']
    template_name = 'login.html'

    def get(self, request):
        if request.user.is_authenticated:
            return redirect('index:index')
        return render(request, self.template_name)

    def post(self, request, *args, **kwargs):
        if request.user.is_authenticated:
            return redirect('index:index')

        username = request.POST.get('username', False)
        password = request.POST.get('password', False)

        if False in [username, password]:
            return render(request, self.template_name, {'error': 'Enter both username and password!'})

        user = authenticate(username=username, password=password)

        if user is None or not user.is_active:
            return render(request, self.template_name, {'error': 'Wrong username or password.'})

        try:
            login(request, user)
            return redirect('index:index')
        except Exception as e:
            return render(request, self.template_name, {'error': f'Something went wrong: {e}'})


class LogoutView(View):
    http_method_names = ['get', ]

    def get(self, request):
        if not request.user.is_authenticated:
            return redirect('home')
        logout(request)
        return redirect('home')


def change_password(request):
    if not request.user.is_authenticated:
        return redirect('home')

    user = request.user

    if request.method == 'GET':
        return render(request, 'change_password.html')

    new_password = request.POST.get('new_password', False)

    if not new_password:
        return redirect('home')

    user.set_password(new_password)
    user.save()
    update_session_auth_hash(request, user)
    return redirect('home')


def delete_member(req):
    if not req.user or not req.user.is_authenticated:
        return None
    user = req.user
    member = Member.objects.get(pk=user.id)
    if member.position != "admin":
        return None
    id_member_to_delete = req.GET.get('id_member_to_delete', None)

    member_to_delete = Member.objects.get(pk=int(id_member_to_delete))
    user_to_delete = member_to_delete.user_profile
    member_to_delete.delete()
    user_to_delete.delete()
    return JsonResponse({"status": "حذف با موفقیت انجام شد"})


class AddNewMember(View):
    """
    Admin can register new user using this view, new user can be a student or a teacher.
    """
    def get(self, request):
        if not request.user.is_authenticated:
            return None
        user = request.user
        member = Member.objects.get(user_profile_id=user.id)
        if member.position != "admin":
            return None
        return render(request, 'register.html')

    def post(self, request):
        if not request.user.is_authenticated:
            return redirect('home')

        username = request.POST.get('username', False)
        password = request.POST.get('password', False)
        first_name = request.POST.get('first_name', False)
        last_name = request.POST.get('last_name', False)
        position = request.POST.get('position', False)
        national_code = request.POST.get('national_code', False)
        department = request.POST.get('department', False)
        profile_picture = request.FILES.get('profile_picture', False)

        if False in (a:=[username, password, first_name, last_name, position, national_code, department, profile_picture]):
            print(a)
            return

        user = User.objects.create_user(username=username, password=password, first_name=first_name, last_name=last_name,)
        user.save()
        member = Member.objects.create(position=position, user_profile_id=user.id, national_code=national_code, profile_picture=profile_picture)

        if position == "student":
            student = Student.objects.create(member_profile_id=member.id, department=department)
            student.save()
        elif position == "teacher":
            teacher = Teacher.objects.create(member_profile_id=member.id, department=department)
            teacher.save()
        return redirect('home')

