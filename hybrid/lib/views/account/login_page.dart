import 'package:flutter/material.dart';
import 'package:hybrid/components/app_submit_button.dart';
import 'package:hybrid/components/app_textfield.dart';
import 'package:reactive_forms/reactive_forms.dart';

class LoginPage extends StatelessWidget {
  final String? username;

  LoginPage({
    Key? key,
    this.username,
  }) : super(key: key);

  final Color rwColor = const Color.fromRGBO(64, 143, 77, 1);
  final TextStyle focusedStyle = const TextStyle(color: Colors.green);
  final TextStyle unfocusedStyle = const TextStyle(color: Colors.grey);

  final loginForm = FormGroup({
    'username': FormControl<String>(
        value: 'superadmin', validators: [Validators.required]),
    'password': FormControl<String>(validators: [Validators.required]),
  });

  login() {
    print('login');
    print(loginForm.value);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Center(
          child: ReactiveForm(
            formGroup: loginForm,
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.stretch,
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                const SizedBox(
                  height: 200,
                  child: Image(
                    image: AssetImage('assets/images/logo.png'),
                  ),
                ),
                const SizedBox(height: 16),
                AppTextField(
                  hintText: 'UserName',
                  formControlName: 'username',
                  validationMessages: (control) => {
                    ValidationMessage.required: 'The username must not be empty'
                  },
                ),
                const SizedBox(height: 16),
                AppTextField(
                  hintText: 'Password',
                  formControlName: 'password',
                  validationMessages: (control) => {
                    ValidationMessage.required: 'The password must not be empty'
                  },
                  obscureText: true,
                ),
                const SizedBox(height: 16),
                AppSubmitButton(
                  label: 'Submit',
                  onPress: () => login,
                )
              ],
            ),
          ),
        ),
      ),
    );
  }
}
