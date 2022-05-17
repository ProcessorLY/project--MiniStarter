import 'package:auto_route/auto_route.dart';
import 'package:flutter/material.dart';
import 'package:hybrid/api/agent.dart';

import '../../components/components.dart';
import '../../config/config.dart';
import '../../models/models.dart';
import 'menu_list.dart';

class HomePage extends StatefulWidget {
  HomePage({Key? key}) : super(key: key);

  // final String title;

  @override
  State<HomePage> createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('widget.title'),
      ),
      body: SafeArea(
          child: Row(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Expanded(
            flex: 2,
            child: leftSideNav(),
          ),
          Expanded(
            flex: 10,
            child: Container(
              color: Colors.green,
              width: double.infinity,
              height: SizeConfig.screenHeight,
            ),
          ),
          // Expanded(
          //     flex: 4,
          //     child: Container(
          //       color: Colors.blue,
          //       width: double.infinity,
          //       height: SizeConfig.screenHeight,
          //     ))
        ],
      )),
    );
  }

  Drawer leftSideNav() {
    return Drawer(
      elevation: 0,
      child: Container(
        color: Colors.red.shade400,
        width: double.infinity,
        height: SizeConfig.screenHeight,
        child: SingleChildScrollView(
            child: Column(
          mainAxisSize: MainAxisSize.max,
          children: [
            Container(
              color: Colors.red.shade900,
              height: 100,
              alignment: Alignment.topCenter,
              padding: const EdgeInsets.only(top: 20),
              child: const Center(
                child: Icon(
                  Icons.home,
                  size: 50,
                ),
              ),
            ),
            const SizedBox(
              height: 10.0,
            ),
            for (AppMenuItem item in menuList)
              Container(
                  width: 120,
                  height: 100,
                  margin: const EdgeInsets.all(10.0),
                  padding: const EdgeInsets.all(8.0),
                  decoration: BoxDecoration(
                      color: Colors.red.shade900,
                      borderRadius: BorderRadius.circular(5.0)),
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.center,
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      Icon(
                        item.icon,
                        size: 40.0,
                      ),
                      // const SizedBox(width: 8.0),
                      Text(
                        item.text!,
                        style: const TextStyle(fontSize: 16.0),
                      )
                    ],
                  )),
          ],
        )),
      ),
    );
  }
}
