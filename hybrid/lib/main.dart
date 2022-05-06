import 'package:dio/dio.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:hybrid/providers/profile_provider.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:hybrid/screens/screens.dart';
import 'config/config.dart';
import 'helpers/app_localizations.dart';
import 'providers/app_provider.dart';
import 'package:flutter_localizations/flutter_localizations.dart';

void main() async {
  Response response;
  var dio = Dio();
  response = await dio.get('https://localhost:5001/api/AuditLogs');
  print(response.data.toString());

  runApp(ProviderScope(child: MyApp()));
}

class MyApp extends ConsumerWidget {
  MyApp({Key? key}) : super(key: key);
  final _appProvider =
      ChangeNotifierProvider<AppProvider>((ref) => AppProvider());

  final _profileProvider = ChangeNotifierProvider<ProfileProvider>((ref) {
    return ProfileProvider();
  });

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    AppProvider appProvider = ref.watch(_appProvider);
    ProfileProvider profileManager = ref.watch(_profileProvider);
    print(appProvider.appLocale);
    ThemeData theme;
    if (profileManager.darkMode) {
      theme = AppTheme.dark();
    } else {
      theme = AppTheme.light();
    }
    return MaterialApp(
      title: 'Mini Starter',
      theme: theme,
      locale: appProvider.appLocale,
      supportedLocales: AppProvider.supportedLanguage,
      localizationsDelegates: const [
        AppLocalizations.delegate,
        GlobalMaterialLocalizations.delegate,
        GlobalWidgetsLocalizations.delegate,
        GlobalCupertinoLocalizations.delegate,
        DefaultCupertinoLocalizations.delegate,
      ],
      home: HomeScreen(title: appProvider.appLocale.toString()),
    );
  }
}
